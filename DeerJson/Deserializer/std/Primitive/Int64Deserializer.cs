using System;

namespace DeerJson.Deserializer.std.Primitive
{
    public class Int64Deserializer : JsonDeserializer<long>
    {
        public static readonly Int64Deserializer Instance = new Int64Deserializer();

        public override long GetNullValue(DeserializeContext ctx)
        {
            return 0;
        }

        public override long Deserialize(JsonParser p, DeserializeContext ctx)
        {
            var v = p.GetNumber();
            var num = Convert.ToInt64(v);
            return num;
        }
    }

    public class UInt64Deserializer : JsonDeserializer<ulong>
    {
        public static readonly UInt64Deserializer Instance = new UInt64Deserializer();

        public override ulong GetNullValue(DeserializeContext ctx)
        {
            return 0;
        }

        public override ulong Deserialize(JsonParser p, DeserializeContext ctx)
        {
            var v = p.GetNumber();
            var num = Convert.ToUInt64(v);
            return num;
        }
    }
}