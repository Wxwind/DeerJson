namespace DeerJson.Serializer
{
    public abstract class JsonSerializer<T> : ISerializer
    {
        void ISerializer.Serialize(object value, JsonGenerator gen, SerializeContext ctx)
        {
            Serialize((T)value, gen, ctx);
        }

        public abstract void Serialize(T value, JsonGenerator gen, SerializeContext ctx);
    }
}