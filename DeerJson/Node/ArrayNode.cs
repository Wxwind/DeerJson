using System;
using System.Collections.Generic;
using System.Text;


namespace DeerJson.Node
{
    public class ArrayNode : JsonNode, IEquatable<ArrayNode>
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

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            sb.Append(Environment.NewLine);
            foreach (var el in m_array)
            {
                sb.Append(el);
                sb.Append(", ");
                sb.Append(Environment.NewLine);
            }

            sb.Append("]");
            return sb.ToString();
        }

        public bool Equals(ArrayNode other)
        {
            if (other is null) return false;
            if (m_array.Count != other.m_array.Count) return false;

            for (var i = 0; i < m_array.Count; i++)
                if (!m_array[i].Equals(other.m_array[i]))
                    return false;

            return true;
        }
    }
}