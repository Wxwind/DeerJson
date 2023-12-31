﻿using System;

namespace DeerJson.Deserializer.std.Primitive
{
    public class DoubleDeserializer : JsonDeserializer<double>
    {
        public static readonly DoubleDeserializer Instance = new DoubleDeserializer();

        public override double GetNullValue(DeserializeContext ctx)
        {
            return 0;
        }

        public override double Deserialize(JsonParser p, DeserializeContext ctx)
        {
            var v = p.GetNumber();
            var num = Convert.ToDouble(v);
            return num;
        }
    }
}