namespace DeerJson.Deserializer
{
    public interface IDeserializer
    {
        object Deserialize(JsonParser p, DeserializeContext ctx);
    }
}