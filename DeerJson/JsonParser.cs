using System;
using System.Collections.Generic;
using DeerJson.AST;

namespace DeerJson
{
    public class JsonParser
    {
        private readonly Lexer m_lexer;
        private          Token m_nowToken;

        public JsonParser(string json)
        {
            m_lexer = new Lexer(json);
            m_lexer.SetInputStr(json);
            m_lexer.GetNextToken();
        }

        public JsonNode Parse(string json)
        {
            return ExecValue();
        }

        private void Match(TokenType type)
        {
            if (m_nowToken.TokenType == type) m_nowToken = m_lexer.GetNextToken();
            else throw new Exception($"syntax error: expected '${type}' but get '{m_nowToken.TokenType}'");
        }

        private bool HasToken(TokenType type)
        {
            return m_nowToken.TokenType == type;
        }

        public bool HasTrailingTokens()
        {
            return !HasToken(TokenType.EOF);
        }

        private JsonNode ExecValue()
        {
            switch (m_nowToken.TokenType)
            {
                case TokenType.STRING:
                    Match(TokenType.STRING);
                    return new StringNode(m_nowToken.Value);

                case TokenType.NUMBER:
                    Match(TokenType.NUMBER);
                    return new NumericNode(m_nowToken.Value);

                case TokenType.TRUE:
                    Match(TokenType.TRUE);
                    return new BooleanNode(true);

                case TokenType.FALSE:
                    Match(TokenType.FALSE);
                    return new BooleanNode(false);

                case TokenType.NULL:
                    Match(TokenType.NULL);
                    return new NullNode();

                case TokenType.LBRACKET:
                    return ExecArray();

                case TokenType.LBRACE:
                    return ExecObject();

                default:
                    throw new Exception($"{m_nowToken.TokenType} is illegal");
            }
        }

        private ArrayNode ExecArray()
        {
            Match(TokenType.LBRACKET);

            if (HasToken(TokenType.RBRACKET)) return new ArrayNode();

            var array = new List<JsonNode>();
            array.Add(ExecValue());
            while (HasToken(TokenType.COMMA))
            {
                Match(TokenType.COMMA);
                array.Add(ExecValue());
            }

            if (HasToken(TokenType.RBRACKET)) return new ArrayNode(array);

            throw new Exception($"missing ']' after {m_nowToken.Value}");
        }

        private ObjectNode ExecObject()
        {
            Match(TokenType.LBRACE);

            if (HasToken(TokenType.RBRACE)) return new ObjectNode();

            var propDic = new Dictionary<string, JsonNode>();
            var (key, value) = ExecPair();
            propDic.Add(key, value);
            while (HasToken(TokenType.COMMA))
            {
                Match(TokenType.COMMA);
                var (k, v) = ExecPair();
                propDic.Add(k, v);
            }

            if (HasToken(TokenType.RBRACE)) return new ObjectNode(propDic);

            throw new Exception($"missing '}}' after {m_nowToken.Value}");
        }

        private (string, JsonNode) ExecPair()
        {
            var key = m_nowToken.Value;
            Match(TokenType.STRING);
            Match(TokenType.COMMA);
            var value = ExecValue();
            return (key, value);
        }
    }
}