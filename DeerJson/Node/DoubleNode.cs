using System;

namespace DeerJson.Node
{
    public class DoubleNode : IEquatable<DoubleNode>
    {
        private double m_value;

        public DoubleNode(double value)
        {
            m_value = value;
        }

        public override string ToString()
        {
            return m_value.ToString();
        }

        public bool Equals(DoubleNode other)
        {
            if (other is null) return false;
            return Math.Abs(m_value - other.m_value) < 0.001;
        }
    }
}