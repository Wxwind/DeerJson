namespace DeerJson.Deserializer.std.Primitive
{
    public class BooleanDeserializer : JsonDeserializer<bool>
    {
        public static readonly BooleanDeserializer Instance    = new BooleanDeserializer();
        private readonly       object              m_nullValue = false;

        public override bool GetNullValue(DeserializeContext ctx)
        {
            return false;
        }

        public override bool Deserialize(JsonParser p, DeserializeContext ctx)
        {
            var v = p.GetBool();
            return v;
        }
    }
}