using System;

namespace DeerJson.Serializer.std
{
    public class EnumSerializer : JsonSerializer<Enum>
    {
        public override void Serialize(Enum value, JsonGenerator gen)
        {
            throw new NotImplementedException();
        }
    }
}