namespace DeerJson.Deserializer
{
    public abstract class JsonKeyDeserializer<T> : IKeyDeserializer
    {
        object IKeyDeserializer.Deserialize(string v, DeserializeContext ctx)
        {
            return Deserialize(v, ctx);
        }

        public abstract T Deserialize(string v, DeserializeContext ctx);
    }
}