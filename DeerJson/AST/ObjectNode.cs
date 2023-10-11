using System.Collections.Generic;

namespace DeerJson.AST
{
    public class ObjectNode : JsonNode
    {
        private Dictionary<string, JsonNode> m_propDic;

        public ObjectNode()
        {
            m_propDic = new Dictionary<string, JsonNode>();
        }

        public ObjectNode(Dictionary<string, JsonNode> propDic)
        {
            m_propDic = propDic;
        }
    }
}