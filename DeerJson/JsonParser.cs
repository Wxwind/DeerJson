using System;
using System.Collections.Generic;
using DeerJson.Node;

namespace DeerJson
{
    public class JsonParser
    {
        private readonly Lexer m_lexer;
        private          Token m_curToken;

        public Token CurToken => m_curToken;
        public int CurLine => m_lexer.CurLine;

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

        public Token GetNextToken(TokenType type)
        {
            if (m_curToken.TokenType == type)
            {
                m_curToken = m_lexer.GetNextToken();
                return m_curToken;
            }

            ReportDetailError($"syntax error: expected '{type}' but get '{m_curToken.TokenType}'");
            return default;
        }

        public string MatchPropName()
        {
            if (m_curToken.TokenType == TokenType.STRING)
            {
                m_curToken = m_lexer.GetNextToken();
                return m_curToken.Value;
            }

            ReportDetailError($"syntax error: expected '{TokenType.STRING}' but get '{m_curToken.TokenType}'");
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

        private void ReportDetailError(string error)
        {
            throw new JsonException($"parser error: {error}. \n In line {m_lexer.CurLine}");
        }
    }
}