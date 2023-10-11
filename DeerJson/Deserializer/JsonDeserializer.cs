namespace DeerJson.Deserializer
{
    public abstract class JsonDeserializer<T>
    {
        public abstract T Deserialize(JsonParser p);
    }
}