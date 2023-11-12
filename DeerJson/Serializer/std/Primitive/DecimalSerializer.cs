namespace DeerJson.Serializer.std.Primitive
{
    public class DecimalSerializer : JsonSerializer<decimal>
    {
        public static DecimalSerializer Instance = new DecimalSerializer();

        public override void Serialize(decimal value, JsonGenerator gen, SerializeContext ctx)
        {
            gen.WriteNumber(value);
        }
    }
}