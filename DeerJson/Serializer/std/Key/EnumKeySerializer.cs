using System;

namespace DeerJson.Serializer.std.Key
{
    public class EnumKeySerializer : JsonSerializer<Enum>
    {
        public static EnumKeySerializer Instance = new EnumKeySerializer();

        public override void Serialize(Enum value, JsonGenerator gen, SerializeContext ctx)
        {
            if (ctx.IsEnabled(JsonFeature.SERIALIZE_UNDERLYING_TYPE_FOR_ENUM))
            {
                gen.WriteMemberName(((int)(object)value).ToString());
            }
            else gen.WriteMemberName(value.ToString());
        }
    }
}