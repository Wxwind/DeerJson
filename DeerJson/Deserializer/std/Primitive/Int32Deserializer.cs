using System;

namespace DeerJson.Deserializer.std.Primitive
{
    public class Int32Deserializer : JsonDeserializer<int>
    {
        public static readonly Int32Deserializer Instance = new Int32Deserializer();

        public override int Deserialize(JsonParser p)
        {
            var v = p.GetNumber();
            var num = Convert.ToInt32(v);
            return num;
        }
    }

    public class UInt32Deserializer : JsonDeserializer<uint>
    {
        public static readonly UInt32Deserializer Instance = new UInt32Deserializer();

        public override uint Deserialize(JsonParser p)
        {
            var v = p.GetNumber();
            var num = Convert.ToUInt32(v);
            return num;
        }
    }
}