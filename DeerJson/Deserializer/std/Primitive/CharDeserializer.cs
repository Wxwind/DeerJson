using System;

namespace DeerJson.Deserializer.std.Primitive
{
    public class CharDeserializer : JsonDeserializer<char>
    {
        public static readonly CharDeserializer Instance = new CharDeserializer();

        public override char Deserialize(JsonParser p)
        {
            var v = p.GetString();
            var str = Convert.ToChar(v);
            return str;
        }
    }
}