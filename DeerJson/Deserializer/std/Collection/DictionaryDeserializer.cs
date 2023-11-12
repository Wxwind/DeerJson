using System;
using System.Collections;

namespace DeerJson.Deserializer.std.Collection
{
    public class DictionaryDeserializer : JsonDeserializer<IDictionary>, IResolvableDeserializer
    {
        private Type m_type;
        private Type m_keyType;
        private Type m_valueType;

        private IDeserializer m_keyDeserializer;
        private IDeserializer m_valueDeserializer;

        public DictionaryDeserializer(Type type, Type keyType, Type valueType)
        {
            m_type = type;
            m_keyType = keyType;
            m_valueType = valueType;
        }

        public override IDictionary Deserialize(JsonParser p, DeserializeContext ctx)
        {
            throw new NotImplementedException();
        }

        public void Resolve(DeserializeContext ctx)
        {
            m_keyDeserializer = ctx.FindDeserializer(m_keyType);
            m_valueDeserializer = ctx.FindDeserializer(m_valueType);
        }
    }
}