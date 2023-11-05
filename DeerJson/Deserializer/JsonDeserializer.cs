namespace DeerJson.Deserializer.std
{
    public abstract class JsonDeserializer<T> : IDeserializer
    {
        object IDeserializer.Deserialize(JsonParser p)
        {
            return Deserialize(p);
        }

        public abstract T Deserialize(JsonParser p);
    }
}