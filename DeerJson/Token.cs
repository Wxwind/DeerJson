using System;

namespace DeerJson
{
    public class Token : IEquatable<Token>
    {
        public Token(TokenType tokenType, string value = "")
        {
            TokenType = tokenType;
            Value = value;
        }

        public TokenType TokenType { get; }
        public string Value { get; }

        public bool Equals(Token other)
        {
            if (other is null) return false;
            return TokenType == other.TokenType && Value == other.Value;
        }

        public override int GetHashCode()
        {
            return TokenType.GetHashCode() ^ Value.GetHashCode();
        }
    }
}