using System;
using System.Collections;

namespace DeerJson.Deserializer.std.Collection
{
    public class DictionaryDeserializer : JsonDeserializer<IDictionary>, IResolvableDeserializer
    {
        private Type m_type;
        private Type m_keyType;
        private Type m_valueType;

        private IKeyDeserializer m_keyDeserializer;
        private IDeserializer    m_valueDeserializer;

        public DictionaryDeserializer(Type type, Type keyType, Type valueType)
        {
            m_type = type;
            m_keyType = keyType;
            m_valueType = valueType;
        }

        public override IDictionary Deserialize(JsonParser p, DeserializeContext ctx)
        {
            p.GetObjectStart();
            var o = Activator.CreateInstance(m_type) as IDictionary;

            string name;

            while ((name = p.GetMemberName()) != null)
            {
                var key = m_keyDeserializer.Deserialize(name, ctx);
                var value = m_valueDeserializer.Deserialize(p, ctx);
                o.Add(key, value);
            }

            p.GetObjectEnd();
            return o;
        }

        public void Resolve(DeserializeContext ctx)
        {
            m_keyDeserializer = ctx.FindStdKeySerializer(m_keyType);
            m_valueDeserializer = ctx.FindDeserializer(m_valueType);
        }
    }
}