using System;

namespace DeerJson.Node
{
    public class NullNode : JsonNode, IEquatable<NullNode>
    {
        public override string ToString()
        {
            return "null";
        }

        public bool Equals(NullNode other)
        {
            if (other is null) return false;
            return true;
        }
    }
}