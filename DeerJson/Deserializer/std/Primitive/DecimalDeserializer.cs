﻿using System;

namespace DeerJson.Deserializer.std.Primitive
{
    public class DecimalDeserializer : JsonDeserializer<decimal>
    {
        public static readonly DecimalDeserializer Instance = new DecimalDeserializer();

        public override decimal Deserialize(JsonParser p)
        {
            var v = p.GetNumber();
            var num = Convert.ToDecimal(v);
            return num;
        }
    }
}