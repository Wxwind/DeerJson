using System;

namespace DeerJson.Serializer.std
{
    public class EnumSerializer : JsonSerializer<Enum>
    {
        public override void Serialize(Enum value, JsonGenerator gen, SerializeContext ctx)
        {
            if (ctx.IsEnabled(JsonFeature.SERIALIZE_UNDERLYING_TYPE_FOR_ENUM))
            {
                gen.WriteNumber((int)(object)value);
            }
            else gen.WriteString(value.ToString());
        }
    }
}