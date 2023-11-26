using System;

namespace DeerJson.Deserializer.std.Primitive
{
    public class FloatDeserializer : JsonDeserializer<float>
    {
        public static readonly FloatDeserializer Instance = new FloatDeserializer();

        public override float GetNullValue(DeserializeContext ctx)
        {
            return 0;
        }

        public override float Deserialize(JsonParser p, DeserializeContext ctx)
        {
            var v = p.GetNumber();
            var num = Convert.ToSingle(v);
            return num;
        }
    }
}