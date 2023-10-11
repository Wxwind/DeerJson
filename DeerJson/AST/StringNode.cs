namespace DeerJson.AST
{
    public class StringNode : JsonNode
    {
        private string m_value;

        public StringNode(string value)
        {
            m_value = value;
        }
    }
}