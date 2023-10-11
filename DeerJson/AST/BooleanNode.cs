namespace DeerJson.AST
{
    public class BooleanNode : JsonNode
    {
        private bool m_value;

        public BooleanNode(bool value)
        {
            m_value = value;
        }
    }
}