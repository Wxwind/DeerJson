using System;

namespace DeerJson.Serializer.std.Collection
{
    public class ArraySerializer : JsonSerializer<Array>, IResolvableSerializer
    {
        private Type        m_type;
        private Type        m_elementType;
        private ISerializer m_elementSerializer;

        public ArraySerializer(Type type, Type elementType)
        {
            m_type = type;
            m_elementType = elementType;
        }

        public override void Serialize(Array value, JsonGenerator gen, SerializeContext ctx)
        {
            gen.WriteArrayStart();

            foreach (var a in value)
            {
                m_elementSerializer.Serialize(a, gen, ctx);
            }

            gen.WriteArrayEnd();
        }

        public void Resolve(SerializeContext ctx)
        {
            m_elementSerializer = ctx.FindSerializer(m_elementType);
        }
    }
}