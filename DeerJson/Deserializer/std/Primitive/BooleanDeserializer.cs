﻿namespace DeerJson.Deserializer.std.Primitive
{
    public class BooleanDeserializer : JsonDeserializer<bool>
    {
        public static readonly BooleanDeserializer Instance = new BooleanDeserializer();

        public override bool Deserialize(JsonParser p, DeserializeContext ctx)
        {
            var v = p.GetBool();
            return v;
        }
    }
}