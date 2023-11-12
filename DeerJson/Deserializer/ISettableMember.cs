using System;
using DeerJson.Deserializer.std;

namespace DeerJson.Deserializer
{
    public interface ISettableMember
    {
        Type Type { get; }

        void SetDeserializer(IDeserializer deser);

        void DeserializeAndSet(JsonParser p, object obj, DeserializeContext ctx);
    }
}