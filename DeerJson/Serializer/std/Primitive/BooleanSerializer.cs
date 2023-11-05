namespace DeerJson.Serializer.std.Primitive
{
    public class BooleanSerializer : JsonSerializer<bool>
    {
        public static BooleanSerializer Instance = new BooleanSerializer();

        public override void Serialize(bool value, JsonGenerator gen)
        {
            gen.WriteBoolean(value);
        }
    }
}