namespace DeerJson.Serializer
{
    public abstract class JsonSerializer<T> : ISerializer
    {
        void ISerializer.Serialize(object value, JsonGenerator gen)
        {
            Serialize((T)value, gen);
        }

        public abstract void Serialize(T value, JsonGenerator gen);
    }
}