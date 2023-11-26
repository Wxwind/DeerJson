using System;

namespace DeerJson.Deserializer.std.Primitive
{
    public class Int16Deserializer : JsonDeserializer<short>
    {
        public static readonly Int16Deserializer Instance = new Int16Deserializer();

        public override short GetNullValue(DeserializeContext ctx)
        {
            return 0;
        }

        public override short Deserialize(JsonParser p, DeserializeContext ctx)
        {
            var v = p.GetNumber();
            var num = Convert.ToInt16(v);
            return num;
        }
    }

    public class UInt16Deserializer : JsonDeserializer<ushort>
    {
        public static readonly UInt16Deserializer Instance = new UInt16Deserializer();

        public override ushort GetNullValue(DeserializeContext ctx)
        {
            return 0;
        }

        public override ushort Deserialize(JsonParser p, DeserializeContext ctx)
        {
            var v = p.GetNumber();
            var num = Convert.ToUInt16(v);
            return num;
        }
    }
}