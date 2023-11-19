using System;

namespace DeerJson.Deserializer.std.Key
{
    public class EnumKeyDeserializer : JsonKeyDeserializer<object>
    {
        private          Type     m_type;
        private readonly Type     m_underLyingType;
        private          string[] m_enumNameList;

        public EnumKeyDeserializer(Type type, Type underLyingType)
        {
            m_type = type;
            m_underLyingType = underLyingType;
            m_enumNameList = Enum.GetNames(m_type);
        }

        public override object Deserialize(string key, DeserializeContext ctx)
        {
            // may be use enum name?
            if (Enum.IsDefined(m_type, key))
            {
                foreach (var name in m_enumNameList)
                {
                    if (name == key)
                    {
                        return Enum.Parse(m_type, key);
                    }
                }
            }

            // or may use enum constant's underlying type like int?
            var num = Convert.ChangeType(key, m_underLyingType);
            if (Enum.IsDefined(m_type, num))
            {
                return Enum.ToObject(m_type, num);
            }

            throw new JsonException($"Enum {m_type} doesn't have constant whose name or base value is {key}");
        }
    }
}