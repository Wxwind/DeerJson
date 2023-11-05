namespace DeerJson.Serializer.std.Primitive
{
    public class FloatSerializer : JsonSerializer<float>
    {
        public static FloatSerializer Instance = new FloatSerializer();

        public override void Serialize(float value, JsonGenerator gen)
        {
            gen.WriteNumber(value);
        }
    }
}