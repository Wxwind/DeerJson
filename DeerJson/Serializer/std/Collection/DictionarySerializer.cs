using System;
using System.Collections;

namespace DeerJson.Serializer.std.Collection
{
    public class DictionarySerializer : JsonSerializer<IDictionary>, IResolvableSerializer
    {
        private Type m_type;
        private Type m_keyType;
        private Type m_valueType;

        private ISerializer m_keySerializer;
        private ISerializer m_valueSerializer;

        public DictionarySerializer(Type type, Type keyType, Type valueType)
        {
            m_type = type;
            m_keyType = keyType;
            m_valueType = valueType;
        }

        public override void Serialize(IDictionary value, JsonGenerator gen, SerializeContext ctx)
        {
            gen.WriteObjectStart();

            foreach (DictionaryEntry kv in value)
            {
                gen.WriteMemberName(kv.Key.ToString());
                m_valueSerializer.Serialize(kv.Value, gen, ctx);
            }

            gen.WriteObjectEnd();
        }

        public void Resolve(SerializeContext ctx)
        {
            m_keySerializer = ctx.FindSerializer(m_keyType);
            m_valueSerializer = ctx.FindSerializer(m_valueType);
        }
    }
}