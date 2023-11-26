using System;

namespace DeerJson.Deserializer.std.Primitive
{
    public class ByteDeserializer : JsonDeserializer<byte>
    {
        public static readonly ByteDeserializer Instance = new ByteDeserializer();

        public override byte GetNullValue(DeserializeContext ctx)
        {
            return 0;
        }
        
        public override byte Deserialize(JsonParser p, DeserializeContext ctx)
        {
            var v = p.GetNumber();
            var num = Convert.ToByte(v);
            return num;
        }
    }

    public class SByteDeserializer : JsonDeserializer<sbyte>
    {
        public static readonly SByteDeserializer Instance = new SByteDeserializer();

        public override sbyte GetNullValue(DeserializeContext ctx)
        {
            return 0;
        }

        public override sbyte Deserialize(JsonParser p, DeserializeContext ctx)
        {
            var v = p.GetNumber();
            var num = Convert.ToSByte(v);
            return num;
        }
    }
}