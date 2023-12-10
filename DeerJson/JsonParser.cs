namespace DeerJson
{
    public class JsonParser
    {
        private readonly Lexer m_lexer;
        
        public int CurLine => m_lexer.CurLine;
        public TokenType CurToken => m_lexer.CurToken;
        public string CurTokenValue => m_lexer.CurTokenValue;

        private ParserContext m_parserContext = new ParserContext();

        public JsonParser(string json)
        {
            m_lexer = new Lexer(json);
            MoveNext();
        }

        public void MoveNext()
        {
            m_lexer.GetNextToken();
        }

        // throw error if token unexpected, move next otherwise
        public void Match(TokenType type)
        {
            if (CurToken == type) MoveNext();
            else ReportDetailError($"syntax error: expected '{type}' but get '{CurToken}'");
        }

        public void GetObjectStart()
        {
            VerifyValueGet();
            Match(TokenType.LBRACE);
            m_parserContext = m_parserContext.NewObjectContext();
        }

        public string GetMemberName()
        {
            if (HasToken(TokenType.RBRACE))
            {
                return null;
            }

            var status = m_parserContext.OnBeforeGetMemberName();

            if (status == ParserContext.Status.EXPECT_VALUE)
            {
                SkipMemberValue();
            }

            if (status == ParserContext.Status.OK_NEED_COMMA)
            {
                Match(TokenType.COMMA);

                if (HasToken(TokenType.RBRACE))
                {
                    ReportDetailError("trailing comma is not allowed");
                    return default;
                }
            }

            var propName = InternalGetMemberName();
            Match(TokenType.COLON); 
            return propName;
        }

        public void SkipMemberValue()
        {
            VerifyValueGet();
            if (CurToken != TokenType.LBRACE && CurToken != TokenType.LBRACKET)
            {
                InternalSkipValue();
                return;
            }

            var deep = 0;

            while (true)
            {
                if (CurToken == TokenType.LBRACE || CurToken == TokenType.LBRACKET)
                {
                    deep++;
                }
                else if (CurToken == TokenType.RBRACE || CurToken == TokenType.RBRACKET)
                {
                    if (--deep == 0)
                    {
                        MoveNext();
                        return;
                    }
                }
                else if (CurToken == TokenType.EOF)
                {
                    ReportDetailError("not enough end token(']','}') while skip children");
                    return;
                }

                MoveNext();
            }
        }

        private void InternalSkipValue()
        {
            m_lexer.GetNextToken();
        }

        public void GetObjectEnd()
        {
            if (!m_parserContext.InObject())
            {
                ReportDetailError($"Current context is not in Object but in {m_parserContext.CurContextType}");
                return;
            }

            Match(TokenType.RBRACE);
            m_parserContext = m_parserContext.GetParentContext(0);
        }

        public void GetArrayStart()
        {
            VerifyValueGet();
            Match(TokenType.LBRACKET);
            m_parserContext = m_parserContext.NewArrayContext();
        }
        
        public void GetArrayEnd()
        {
            if (!m_parserContext.InArray())
            {
                ReportDetailError($"Current context is not in Array but in {m_parserContext.CurContextType}");
                return;
            }

            Match(TokenType.RBRACKET);
            m_parserContext = m_parserContext.GetParentContext(1);
        }

        private string InternalGetMemberName()
        {
            if (CurToken == TokenType.STRING)
            {
                var str = CurTokenValue;
                MoveNext();
                return str;
            }

            ReportDetailError($"syntax error: expected '{TokenType.STRING}' but get '{CurToken}'");
            return default;
        }

        public void GetValue()
        {
            VerifyValueGet();
            if (CurToken == TokenType.STRING || CurToken == TokenType.NUMBER ||
                CurToken == TokenType.NULL ||
                CurToken == TokenType.TRUE || CurToken == TokenType.FALSE)
            {
                MoveNext();
        
            }

            ReportDetailError(
                $"syntax error: expected '{TokenType.STRING}' | '{TokenType.NUMBER}' | '{TokenType.TRUE}' | '{TokenType.FALSE}' | '{TokenType.NULL}' but get '{CurToken}'");
        }
        
        public string GetString()
        {
            VerifyValueGet();
            if (CurToken == TokenType.STRING)
            {
                var str = CurTokenValue;
                MoveNext();
                return str;
            }

            ReportDetailError($"syntax error: expected '{TokenType.STRING}' but get '{CurToken}'");
            return default;
        }

        public string GetNumber()
        {
            VerifyValueGet();
            if (CurToken == TokenType.NUMBER)
            {
                var str = CurTokenValue;
                MoveNext();
                return str;
            }

            ReportDetailError($"syntax error: expected '{TokenType.NUMBER}' but get '{CurToken}'");
            return default;
        }

        public bool GetBool()
        {
            VerifyValueGet();
            if (CurToken == TokenType.TRUE)
            {
                MoveNext();
                return true;
            }

            if (CurToken == TokenType.FALSE)
            {
                MoveNext();
                return false;
            }

            ReportDetailError($"syntax error: expected 'True or False' but get '{CurToken}'");
            return default;
        }

        public void GetNull()
        {
            VerifyValueGet();
            if (HasToken(TokenType.NULL))
            {
                Match(TokenType.NULL);
            }
        }

        // return curToken, help to skip comma and colon (used only to handle with null value of array for now).
        public TokenType GetNextToken()
        {
            // Get value after member name
            if (m_parserContext.InObject() && m_parserContext.HasParsedName)
            {
                Match(TokenType.COLON);
                return CurToken;
            }
            else
            {
                if (CurToken == TokenType.RBRACE || CurToken == TokenType.RBRACKET)
                {
                    return CurToken;
                }

                var t = CurToken;

                if (m_parserContext.ExpectComma())
                {
                    Match(TokenType.COMMA);
                    // TODO: feat: jsonfeature.allowtrailingcomma
                    // if (t == TokenType.RBRACE||t == TokenType.RBRACKET)
                    // {
                    //     
                    // }
                    return CurToken;
                }

                return t;
            }
        }

        // return true if token expected
        public bool HasToken(TokenType type)
        {
            return CurToken == type;
        }

        public bool HasTrailingTokens()
        {
            return !HasToken(TokenType.EOF);
        }

        private void VerifyValueGet()
        {
            var status = m_parserContext.OnBeforeGetValue();
            switch (status)
            {
                case ParserContext.Status.OK_NEED_COMMA:
                    Match(TokenType.COMMA);
                    break;
                case ParserContext.Status.EXPECT_NAME:
                    throw new JsonException(
                        $"must call GetMemberName() before GetValueAsXXX() or GetXXX() in context {m_parserContext.CurContextType}");
                default:
                    break;
            }
        }
        
        private void ReportDetailError(string error)
        {
            throw new JsonException($"parser error: {error}. \n In line {m_lexer.CurLine}");
        }
        
    }
}