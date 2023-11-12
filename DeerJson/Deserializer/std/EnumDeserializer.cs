using System;

namespace DeerJson.Deserializer.std
{
    public class EnumDeserializer : JsonDeserializer<object>
    {
        private readonly Type m_type;
        private readonly Type m_underLyingType;

        public EnumDeserializer(Type enumType, Type underLyingType)
        {
            m_type = enumType;
            m_underLyingType = underLyingType;
        }

        public override object Deserialize(JsonParser p, DeserializeContext ctx)
        {
            if (p.CurToken.TokenType == TokenType.NUMBER)
            {
                var v = p.GetNumber();
                var num = Convert.ToInt32(v);
                if (Enum.IsDefined(m_type, num))
                {
                    return Enum.ToObject(m_type, num);
                }

                throw new JsonException($"Enum {m_type} doesn't have enum constant whose base value equals '{num}'");
            }

            if (p.CurToken.TokenType == TokenType.STRING)
            {
                var v = p.GetString();
                if (Enum.IsDefined(m_type, v))
                {
                    return Enum.Parse(m_type, v);
                }

                throw new JsonException($"Enum {m_type} doesn't have constant whose name is {v}");
            }

            throw new JsonException($"Deserialization of enum need value type of string or number");
        }
    }
}