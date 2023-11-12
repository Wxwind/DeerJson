namespace DeerJson.Deserializer.std
{
    public interface IDeserializer
    {
        object Deserialize(JsonParser p, DeserializeContext ctx);
    }
}