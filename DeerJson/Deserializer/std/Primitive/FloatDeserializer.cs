using System;

namespace DeerJson.Deserializer.std.Primitive
{
    public class FloatDeserializer : JsonDeserializer<float>
    {
        public static readonly FloatDeserializer Instance = new FloatDeserializer();

        public override float Deserialize(JsonParser p)
        {
            var v = p.GetNumber();
            var num = Convert.ToSingle(v);
            return num;
        }
    }
}