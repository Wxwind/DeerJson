using System;

namespace DeerJson.Deserializer.std.Primitive
{
    public class CharDeserializer : JsonDeserializer<char>
    {
        public static readonly CharDeserializer Instance = new CharDeserializer();

        public override char GetNullValue(DeserializeContext ctx)
        {
            return '\0';
        }

        public override char Deserialize(JsonParser p, DeserializeContext ctx)
        {
            var v = p.GetString();
            char str;
            if (v == @"\u0000" || v == @"\0")
            {
                str = '\0';
                return str;
            }

            str = Convert.ToChar(v);
            return str;
        }
    }
}