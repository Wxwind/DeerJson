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
            var t = p.GetValue();
            var v = t.Value;

            if (t.TokenType == TokenType.NUMBER)
            {
                var num = Convert.ToInt32(v);
                if (Enum.IsDefined(m_type, num))
                {
                    return Enum.ToObject(m_type, num);
                }

                throw new JsonException($"Enum {m_type} doesn't have enum constant whose base value equals '{num}'");
            }

            if (t.TokenType == TokenType.STRING)
            {
                if (Enum.IsDefined(m_type, v))
                {
                    return Enum.Parse(m_type, v);
                }

                throw new JsonException($"Enum {m_type} doesn't have constant whose name is {v}");
            }

            throw new JsonException(
                $"Deserialization of enum need value type of string or number, but get {p.CurToken.TokenType}");
        }
    }
}