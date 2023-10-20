using System;
using System.Collections.Generic;

namespace DeerJson.Node
{
    public class IntNode : IEquatable<IntNode>
    {
        private int m_value;

        public IntNode(int value)
        {
            m_value = value;
        }

        public override string ToString()
        {
            return m_value.ToString();
        }

        public bool Equals(IntNode other)
        {
            if (other is null) return false;
            return m_value == other.m_value;
        }
    }
}