namespace DeerJson.Serializer.std.Key
{
    public class StdKeySerializer : JsonSerializer<object>
    {
        public static StdKeySerializer Instance = new StdKeySerializer();

        public override void Serialize(object value, JsonGenerator gen, SerializeContext ctx)
        {
            gen.WriteMemberName(value.ToString());
        }
    }
}