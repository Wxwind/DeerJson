namespace DeerJson.Serializer.std.Key
{
    public class StringKeySerializer : JsonSerializer<object>
    {
        public static StringKeySerializer Instance = new StringKeySerializer();

        public override void Serialize(object value, JsonGenerator gen, SerializeContext ctx)
        {
            gen.WriteMemberName(value.ToString());
        }
    }
}