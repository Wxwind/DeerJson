using System;

namespace DeerJson.Deserializer.std
{
    public abstract class JsonDeserializer<T> : IDeserializer
    {
        private readonly Type m_type;
        
        object IDeserializer.Deserialize(JsonParser p, DeserializeContext ctx)
        {
            return Deserialize(p, ctx);
        }

        object IDeserializer.GetNullValue(DeserializeContext ctx)
        {
            return GetNullValue(ctx);
        }

        public abstract T GetNullValue(DeserializeContext ctx);

        public abstract T Deserialize(JsonParser p, DeserializeContext ctx);
    }
}