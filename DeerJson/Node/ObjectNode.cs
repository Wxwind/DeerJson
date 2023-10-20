using System;
using System.Collections.Generic;
using System.Text;

namespace DeerJson.Node
{
    public class ObjectNode : JsonNode, IEquatable<ObjectNode>
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

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("{");
            sb.Append(Environment.NewLine);
            foreach (var pair in m_propDic)
            {
                sb.Append(pair.Key);
                sb.Append(" : ");
                sb.Append(pair.Value);
                sb.Append(Environment.NewLine);
            }

            sb.Append("}");
            return sb.ToString();
        }

        public bool Equals(ObjectNode other)
        {
            if (other is null) return false;
            if (m_propDic.Count != other.m_propDic.Count) return false;

            foreach (var pair in m_propDic)
                if (!(other.m_propDic.ContainsKey(pair.Key) && other.m_propDic[pair.Key] == pair.Value))
                    return false;

            return true;
        }
    }
}