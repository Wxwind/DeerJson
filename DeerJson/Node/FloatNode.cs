using System;

namespace DeerJson.Node
{
    public class FloatNode : IEquatable<FloatNode>
    {
        private float m_value;

        public FloatNode(float value)
        {
            m_value = value;
        }

        public override string ToString()
        {
            return m_value.ToString();
        }

        public bool Equals(FloatNode other)
        {
            if (other is null) return false;
            return Math.Abs(m_value - other.m_value) < 0.001f;
        }
    }
}