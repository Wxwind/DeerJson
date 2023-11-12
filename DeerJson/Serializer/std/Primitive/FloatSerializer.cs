namespace DeerJson.Serializer.std.Primitive
{
    public class FloatSerializer : JsonSerializer<float>
    {
        public static FloatSerializer Instance = new FloatSerializer();

        public override void Serialize(float value, JsonGenerator gen, SerializeContext ctx)
        {
            gen.WriteNumber(value);
        }
    }
}