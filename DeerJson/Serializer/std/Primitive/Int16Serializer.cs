namespace DeerJson.Serializer.std.Primitive
{
    public class Int16Serializer : JsonSerializer<short>
    {
        public static Int16Serializer Instance = new Int16Serializer();

        public override void Serialize(short value, JsonGenerator gen)
        {
            gen.WriteNumber(value);
        }
    }

    public class UInt16Serializer : JsonSerializer<ushort>
    {
        public static UInt16Serializer Instance = new UInt16Serializer();

        public override void Serialize(ushort value, JsonGenerator gen)
        {
            gen.WriteNumber(value);
        }
    }
}