namespace DeerJson.AST
{
    public class NumericNode : JsonNode
    {
        private string m_value;

        public NumericNode(string value)
        {
            m_value = value;
        }
    }
}