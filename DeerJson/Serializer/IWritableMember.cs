using System;

namespace DeerJson.Serializer
{
    public interface IWritableMember
    {
        Type Type { get; }
        string Name { get; }

        void SetSerializer(ISerializer ser);

        void SerializeAndWrite(JsonGenerator p, object obj, SerializeContext ctx);
    }
}