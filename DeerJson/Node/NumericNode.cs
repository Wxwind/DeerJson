using System;

namespace DeerJson.Node
{
    public class NumericNode : JsonNode, IEquatable<NumericNode>
    {
        private string m_value;

        public NumericNode(string value)
        {
            m_value = value;
        }

        public override string ToString()
        {
            return m_value;
        }

        public bool Equals(NumericNode other)
        {
            if (other is null) return false;
            return m_value == other.m_value;
        }
    }
}