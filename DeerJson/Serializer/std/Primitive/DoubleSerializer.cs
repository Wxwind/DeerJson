namespace DeerJson.Serializer.std.Primitive
{
    public class DoubleSerializer : JsonSerializer<double>
    {
        public static DoubleSerializer Instance = new DoubleSerializer();

        public override void Serialize(double value, JsonGenerator gen)
        {
            gen.WriteNumber(value);
        }
    }
}