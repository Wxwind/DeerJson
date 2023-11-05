namespace DeerJson.Serializer.std.Primitive
{
    public class Int64Serializer : JsonSerializer<long>
    {
        public static Int64Serializer Instance = new Int64Serializer();

        public override void Serialize(long value, JsonGenerator gen)
        {
            gen.WriteNumber(value);
        }
    }

    public class UInt64Serializer : JsonSerializer<ulong>
    {
        public static UInt64Serializer Instance = new UInt64Serializer();

        public override void Serialize(ulong value, JsonGenerator gen)
        {
            gen.WriteNumber(value);
        }
    }
}