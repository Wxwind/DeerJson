namespace DeerJson.Deserializer.std
{
    public class StringDeserializer : JsonDeserializer<string>
    {
        public static readonly StringDeserializer Instance = new StringDeserializer();
        
        public override string Deserialize(JsonParser p)
        {
            if (p.HasToken(TokenType.STRING))
            {
                return p.GetNextToken(TokenType.STRING).Value;
            }

            throw new JsonException("Couldn't found string value");
        }
    }
}