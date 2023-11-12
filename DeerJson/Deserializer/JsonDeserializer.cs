namespace DeerJson.Deserializer.std
{
    public abstract class JsonDeserializer<T> : IDeserializer
    {
        object IDeserializer.Deserialize(JsonParser p, DeserializeContext ctx)
        {
            return Deserialize(p, ctx);
        }

        public abstract T Deserialize(JsonParser p, DeserializeContext ctx);
    }
}