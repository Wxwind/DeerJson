using System;

namespace DeerJson.Node
{
    public class BooleanNode : JsonNode, IEquatable<BooleanNode>
    {
        private bool m_value;

        public BooleanNode(bool value)
        {
            m_value = value;
        }

        public override string ToString()
        {
            return m_value.ToString();
        }

        public bool Equals(BooleanNode other)
        {
            if (other is null) return false;
            return m_value == other.m_value;
        }
    }
}