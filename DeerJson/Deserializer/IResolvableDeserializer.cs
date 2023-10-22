namespace DeerJson.Deserializer
{
    public interface IResolvableDeserializer
    {
        void Resolve(DeserializeContext ctx);
    }
}