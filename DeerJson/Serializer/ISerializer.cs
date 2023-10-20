namespace DeerJson.Serializer
{
    public interface ISerializer
    {
        void Serialize(object value, JsonGenerator gen);
    }
}