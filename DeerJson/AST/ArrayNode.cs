using System.Collections.Generic;

namespace DeerJson.AST
{
    public class ArrayNode : JsonNode
    {
        private List<JsonNode> m_array;

        public ArrayNode()
        {
            m_array = new List<JsonNode>();
        }

        public ArrayNode(List<JsonNode> array)
        {
            m_array = array;
        }
    }
}