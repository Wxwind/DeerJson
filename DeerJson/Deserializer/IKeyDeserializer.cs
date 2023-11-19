namespace DeerJson.Deserializer
{
    public interface IKeyDeserializer
    {
        object Deserialize(string p, DeserializeContext ctx);
    }
}