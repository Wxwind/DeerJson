namespace DeerJson
{
    public class JsonParser
    {
        private readonly Lexer m_lexer;
        private          Token m_curToken;

        public Token CurToken => m_curToken;
        public int CurLine => m_lexer.CurLine;

        private ParserContext m_parserContext = new ParserContext();

        public JsonParser(string json)
        {
            m_lexer = new Lexer(json);
            m_curToken = m_lexer.GetNextToken();
        }

        // throw error if token unexpected, move next otherwise
        public void Match(TokenType type)
        {
            if (m_curToken.TokenType == type) m_curToken = m_lexer.GetNextToken();
            else ReportDetailError($"syntax error: expected '{type}' but get '{m_curToken.TokenType}'");
        }

        public void GetObjectStart()
        {
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
            return propName;
        }

        public void SkipMemberValue()
        {
            if (m_curToken.TokenType != TokenType.LBRACE && m_curToken.TokenType != TokenType.LBRACKET)
            {
                InternalSkipValue();
                return;
            }

            var deep = 1;

            while (true)
            {
                if (m_curToken.TokenType == TokenType.LBRACE || m_curToken.TokenType == TokenType.LBRACKET)
                {
                    deep++;
                }
                else if (m_curToken.TokenType == TokenType.RBRACE || m_curToken.TokenType == TokenType.RBRACKET)
                {
                    if (--deep == 0)
                    {
                        m_curToken = m_lexer.GetNextToken();
                        return;
                    }
                }
                else if (m_curToken.TokenType == TokenType.EOF)
                {
                    ReportDetailError("not enough end token(']','}') while skip children");
                    return;
                }

                m_curToken = m_lexer.GetNextToken();
            }
        }

        private void InternalSkipValue()
        {
            var token = m_lexer.GetNextToken();
        }

        public void GetObjectEnd()
        {
            if (!m_parserContext.InObject())
            {
                ReportDetailError($"Current context is not in Object but in {m_parserContext.CurContextType}");
                return;
            }

            Match(TokenType.RBRACE);
            m_parserContext = m_parserContext.GetParentContext();
        }

        public void GetArrayStart()
        {
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
            m_parserContext = m_parserContext.GetParentContext();
        }

        private string InternalGetMemberName()
        {
            if (m_curToken.TokenType == TokenType.STRING)
            {
                var str = m_curToken.Value;
                m_curToken = m_lexer.GetNextToken();
                return str;
            }

            ReportDetailError($"syntax error: expected '{TokenType.STRING}' but get '{m_curToken.TokenType}'");
            return default;
        }

        public Token GetValue()
        {
            VerifyValueGet();
            if (m_curToken.TokenType == TokenType.STRING || m_curToken.TokenType == TokenType.NUMBER ||
                m_curToken.TokenType == TokenType.NULL ||
                m_curToken.TokenType == TokenType.TRUE || m_curToken.TokenType == TokenType.FALSE)
            {
                var t = m_curToken;
                m_curToken = m_lexer.GetNextToken();
                return t;
            }

            ReportDetailError(
                $"syntax error: expected '{TokenType.STRING}' | '{TokenType.NUMBER}' | '{TokenType.TRUE}' | '{TokenType.FALSE}' | '{TokenType.NULL}' but get '{m_curToken.TokenType}'");
            return default;
        }
        
        public string GetString()
        {
            VerifyValueGet();
            if (m_curToken.TokenType == TokenType.STRING)
            {
                var str = m_curToken.Value;
                m_curToken = m_lexer.GetNextToken();
                return str;
            }

            ReportDetailError($"syntax error: expected '{TokenType.STRING}' but get '{m_curToken.TokenType}'");
            return default;
        }

        public string GetNumber()
        {
            VerifyValueGet();
            if (m_curToken.TokenType == TokenType.NUMBER)
            {
                var str = m_curToken.Value;
                m_curToken = m_lexer.GetNextToken();
                return str;
            }

            ReportDetailError($"syntax error: expected '{TokenType.NUMBER}' but get '{m_curToken.TokenType}'");
            return default;
        }

        public bool GetBool()
        {
            VerifyValueGet();
            if (m_curToken.TokenType == TokenType.TRUE)
            {
                m_curToken = m_lexer.GetNextToken();
                return true;
            }

            if (m_curToken.TokenType == TokenType.FALSE)
            {
                m_curToken = m_lexer.GetNextToken();
                return false;
            }

            ReportDetailError($"syntax error: expected 'True or False' but get '{m_curToken.TokenType}'");
            return default;
        }

        // return true if token expected
        public bool HasToken(TokenType type)
        {
            return m_curToken.TokenType == type;
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
                case ParserContext.Status.OK_NEED_COLON:
                    Match(TokenType.COLON);
                    break;
                case ParserContext.Status.OK_NEED_COMMA:
                    Match(TokenType.COMMA);
                    break;
                case ParserContext.Status.EXPECT_NAME:
                    throw new JsonException(
                        $"must call GetMemberName() after GetValueAsXXX() or GetXXX() in context {m_parserContext.CurContextType}");
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