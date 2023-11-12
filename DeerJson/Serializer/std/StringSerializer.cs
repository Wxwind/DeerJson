namespace DeerJson.Serializer.std
{
    public class StringSerializer : JsonSerializer<string>
    {
        public static StringSerializer Instance = new StringSerializer();

        public override void Serialize(string value, JsonGenerator gen, SerializeContext ctx)
        {
            gen.WriteString(value);
        }
    }
}