using System.Collections.Generic;
using DeerJson.Node;

namespace DeerJson.Deserializer.std
{
    public class JsonNodeDeserializer : JsonDeserializer<JsonNode>
    {
        public override JsonNode Deserialize(JsonParser p, DeserializeContext ctx)
        {
            return ExecValue(p);
        }

        public override JsonNode GetNullValue(DeserializeContext ctx)
        {
            return null;
        }

        private JsonNode ExecValue(JsonParser p)
        {
            switch (p.CurToken)
            {
                case TokenType.STRING:
                {
                    var value = p.CurTokenValue;
                    p.Match(TokenType.STRING);
                    return new StringNode(value);
                }

                case TokenType.NUMBER:
                {
                    var value = p.CurTokenValue;
                    p.Match(TokenType.NUMBER);
                    return new NumericNode(value);
                }

                case TokenType.TRUE:
                    p.Match(TokenType.TRUE);
                    return new BooleanNode(true);

                case TokenType.FALSE:
                    p.Match(TokenType.FALSE);
                    return new BooleanNode(false);

                case TokenType.NULL:
                    p.Match(TokenType.NULL);
                    return new NullNode();

                case TokenType.LBRACKET:
                    return ExecArray(p);

                case TokenType.LBRACE:
                    return ExecObject(p);

                default:
                    ReportDetailError(p, $"{p.CurToken} is illegal");
                    return default;
            }
        }

        private ArrayNode ExecArray(JsonParser p)
        {
            p.Match(TokenType.LBRACKET);

            if (p.HasToken(TokenType.RBRACKET))
            {
                var node = new ArrayNode();
                p.Match(TokenType.RBRACKET);
                return node;
            }

            var array = new List<JsonNode>();
            array.Add(ExecValue(p));
            while (p.HasToken(TokenType.COMMA))
            {
                p.Match(TokenType.COMMA);
                array.Add(ExecValue(p));
            }

            if (p.HasToken(TokenType.RBRACKET))
            {
                var node = new ArrayNode(array);
                p.Match(TokenType.RBRACKET);
                return node;
            }

            ReportDetailError(p, $"missing ']' after {p.CurTokenValue}");
            return default;
        }

        private ObjectNode ExecObject(JsonParser p)
        {
            p.Match(TokenType.LBRACE);

            if (p.HasToken(TokenType.RBRACE))
            {
                var node = new ObjectNode();
                p.Match(TokenType.RBRACE);
                return node;
            }

            var propDic = new Dictionary<string, JsonNode>();
            var (key, value) = ExecPair(p);
            propDic.Add(key, value);
            while (p.HasToken(TokenType.COMMA))
            {
                p.Match(TokenType.COMMA);
                var (k, v) = ExecPair(p);
                propDic.Add(k, v);
            }

            if (p.HasToken(TokenType.RBRACE))
            {
                var node = new ObjectNode(propDic);
                p.Match(TokenType.RBRACE);
                return node;
            }


            ReportDetailError(p, $"missing '}}' after {p.CurTokenValue}");
            return default;
        }

        private (string, JsonNode) ExecPair(JsonParser p)
        {
            var key = p.CurTokenValue;
            p.Match(TokenType.STRING);
            p.Match(TokenType.COLON);
            var value = ExecValue(p);
            return (key, value);
        }

        private void ReportDetailError(JsonParser p, string error)
        {
            throw new JsonException($"parser error: {error}. \n In line {p.CurLine}");
        }
    }
}