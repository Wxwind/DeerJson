using System;
using DeerJson.Deserializer.std;

namespace DeerJson.Deserializer
{
    public class DeserializerFactory
    {
        public IDeserializer CreateObjectDeserializer(Type type)
        {
            return new ObjectDeserializer(type);
        }

        public IDeserializer CreateJsonObjectDeserializer()
        {
            return new JsonNodeDeserializer();
        }

        public IDeserializer CreateEnumDeserializer(Type type)
        {
            var e = Enum.GetUnderlyingType(type);
            return new EnumDeserializer(type);
        }
    }
}