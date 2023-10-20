using System;

namespace DeerJson.Node
{
    public class StringNode : JsonNode, IEquatable<StringNode>
    {
        private string m_value;

        public StringNode(string value)
        {
            m_value = value;
        }

        public override string ToString()
        {
            return m_value;
        }

        public bool Equals(StringNode other)
        {
            if (other is null) return false;
            return m_value == other.m_value;
        }
    }
}