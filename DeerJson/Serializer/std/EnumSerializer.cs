using System;

namespace DeerJson.Serializer.std
{
    public class EnumSerializer : JsonSerializer<Enum>
    {
        public override void Serialize(Enum value, JsonGenerator gen)
        {
            gen.WriteNumber((int)(object)value);
            // gen.WriteString(value.ToString());
        }
    }
}