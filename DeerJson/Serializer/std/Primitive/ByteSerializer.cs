namespace DeerJson.Serializer.std.Primitive
{
    public class ByteSerializer : JsonSerializer<byte>
    {
        public static ByteSerializer Instance = new ByteSerializer();

        public override void Serialize(byte value, JsonGenerator gen)
        {
            gen.WriteNumber(value);
        }
    }

    public class SByteSerializer : JsonSerializer<sbyte>
    {
        public static SByteSerializer Instance = new SByteSerializer();

        public override void Serialize(sbyte value, JsonGenerator gen)
        {
            gen.WriteNumber(value);
        }
    }
}