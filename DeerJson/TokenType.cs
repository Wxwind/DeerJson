namespace DeerJson
{
    public enum TokenType
    {
        EOF,
        STRING,
        NUMBER,
        LBRACKET, // '['
        RBRACKET,
        LBRACE, // '{'
        RBRACE,
        COLON,
        COMMA,
        NULL,
        TRUE,
        FALSE
    }
}