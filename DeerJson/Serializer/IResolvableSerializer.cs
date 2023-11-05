namespace DeerJson.Serializer
{
    public interface IResolvableSerializer
    {
        void Resolve(SerializeContext ctx);
    }
}