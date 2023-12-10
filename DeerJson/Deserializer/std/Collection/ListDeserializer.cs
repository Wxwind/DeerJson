using System;
using System.Collections;

namespace DeerJson.Deserializer.std.Collection
{
    public class ListDeserializer : JsonDeserializer<IList>, IResolvableDeserializer
    {
        private Type          m_type;
        private Type          m_elementType;
        private IDeserializer m_elementDeserializer;

        public ListDeserializer(Type type, Type elementType)
        {
            m_type = type;
            m_elementType = elementType;
        }

        public override IList GetNullValue(DeserializeContext ctx)
        {
            return null;
        }

        public override IList Deserialize(JsonParser p, DeserializeContext ctx)
        {
            var list = Activator.CreateInstance(m_type) as IList;

            p.GetArrayStart();

            while (p.GetNextToken() != TokenType.RBRACKET)
            {
                object el;
                if (p.HasToken(TokenType.NULL))
                {
                    if (m_elementType.IsPrimitive && ctx.IsEnabled(JsonFeature.DESERIALIZE_FAIL_ON_NULL_FOR_PRIMITIVES))
                    {
                        throw new JsonException(
                            "cannot deserialize null for primitive type.(set DESERIALIZE_FAIL_ON_NULL_FOR_PRIMITIVES = true to allow)");
                    }

                    el = m_elementDeserializer.GetNullValue(ctx);
                    p.GetNull();
                }
                else el = m_elementDeserializer.Deserialize(p, ctx);
                list.Add(el);
            }

            p.GetArrayEnd();
            
            return list;
        }

        public void Resolve(DeserializeContext ctx)
        {
            m_elementDeserializer = ctx.FindDeserializer(m_elementType);
        }
    }
}