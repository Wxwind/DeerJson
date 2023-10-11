namespace DeerJson.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase("lexerButWrongParser.json")]
    public void Lexer_Example1(string jsonName)
    {
        var filePath = Path.Combine("../../../TestJson", jsonName);
        var json = File.ReadAllText(filePath);
        var a = new Lexer(json);
        var t1 = a.GetNextToken();
        Assert.That(new Token(TokenType.LBRACE), Is.EqualTo(t1));
        var t2 = a.GetNextToken();
        Assert.That(new Token(TokenType.STRING, "hello"), Is.EqualTo(t2));
        var t3 = a.GetNextToken();
        Assert.That(new Token(TokenType.RBRACE), Is.EqualTo(t3));
        var t4 = a.GetNextToken();
        Assert.That(new Token(TokenType.EOF), Is.EqualTo(t4));
    }
}