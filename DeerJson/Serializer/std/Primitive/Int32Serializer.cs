namespace DeerJson.Serializer.std.Primitive
{
    public class Int32Serializer : JsonSerializer<int>
    {
        public static Int32Serializer Instance = new Int32Serializer();

        public override void Serialize(int value, JsonGenerator gen, SerializeContext ctx)
        {
            gen.WriteNumber(value);
        }
    }

    public class UInt32Serializer : JsonSerializer<uint>
    {
        public static UInt32Serializer Instance = new UInt32Serializer();

        public override void Serialize(uint value, JsonGenerator gen, SerializeContext ctx)
        {
            gen.WriteNumber(value);
        }
    }
}