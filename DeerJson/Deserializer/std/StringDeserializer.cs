using System;

namespace DeerJson.Deserializer.std
{
    public class StringDeserializer : JsonDeserializer<string>
    {
        public static readonly StringDeserializer Instance = new StringDeserializer();
        
        public override string Deserialize(JsonParser p)
        {
            var v = p.GetString();
            var str = Convert.ToString(v);
            return str;
        }
    }
}