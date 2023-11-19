using System;

namespace DeerJson.Deserializer.std.Key
{
    public class StdKeyDeserializer : JsonKeyDeserializer<object>
    {
        private Type m_type;

        public StdKeyDeserializer(Type type)
        {
            m_type = type;
        }

        public override object Deserialize(string key, DeserializeContext ctx)
        {
            return Convert.ChangeType(key, m_type);
        }
    }
}