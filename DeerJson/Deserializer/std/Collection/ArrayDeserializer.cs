using System;
using System.Collections.Generic;

namespace DeerJson.Deserializer.std.Collection
{
    public class ArrayDeserializer : JsonDeserializer<Array>, IResolvableDeserializer
    {
        private Type          m_type;
        private Type          m_elementType;
        private IDeserializer m_elementDeserializer;

        public ArrayDeserializer(Type type, Type elementType)
        {
            m_type = type;
            m_elementType = elementType;
        }

        public override Array Deserialize(JsonParser p, DeserializeContext ctx)
        {
            var list = new List<object>();

            var arr = Array.CreateInstance(m_type, list.Count);
            for (var i = 0; i < list.Count; i++)
            {
                arr.SetValue(list[i], i);
            }

            return arr;
        }

        public void Resolve(DeserializeContext ctx)
        {
            m_elementDeserializer = ctx.FindDeserializer(m_elementType);
        }
    }
}