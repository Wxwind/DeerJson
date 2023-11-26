using System;
using System.Reflection;

namespace DeerJson.Deserializer
{
    public class SettableField : ISettableMember
    {
        private          IDeserializer m_valueDeserializer;
        private readonly FieldInfo     m_fieldInfo;
        public Type Type { get; }


        public SettableField(FieldInfo pi)
        {
            m_fieldInfo = pi;
            Type = pi.FieldType;
        }

        public void SetDeserializer(IDeserializer deser)
        {
            m_valueDeserializer = deser;
        }

        public void DeserializeAndSet(JsonParser p, object obj, DeserializeContext ctx)
        {
            object v;
            if (p.HasToken(TokenType.NULL))
            {
                if (Type.IsPrimitive && ctx.IsEnabled(JsonFeature.DESERIALIZE_FAIL_ON_NULL_FOR_PRIMITIVES))
                {
                    throw new JsonException(
                        "cannot deserialize null for primitive type.(set DESERIALIZE_FAIL_ON_NULL_FOR_PRIMITIVES = true to allow)");
                }

                v = m_valueDeserializer.GetNullValue(ctx);
                p.GetNull();
            }
            else
            {
                v = m_valueDeserializer.Deserialize(p, ctx);
            }
            m_fieldInfo.SetValue(obj, v);
        }
    }
}