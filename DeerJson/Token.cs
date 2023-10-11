namespace DeerJson
{
    public class Token
    {
        public Token(TokenType tokenType, string value = "")
        {
            TokenType = tokenType;
            Value = value;
        }

        public TokenType TokenType { get; }
        public string Value { get; }

        public override bool Equals(object obj)
        {
            if (obj is Token t) return TokenType == t.TokenType && Value == t.Value;
            return false;
        }

        public override int GetHashCode()
        {
            return TokenType.GetHashCode() ^ Value.GetHashCode();
        }
    }
}