namespace DeerJson.Serializer.std.Primitive
{
    public class CharSerializer : JsonSerializer<char>
    {
        public static CharSerializer Instance = new CharSerializer();

        public override void Serialize(char value, JsonGenerator gen, SerializeContext ctx)
        {
            gen.WriteString(value);
        }
    }
}