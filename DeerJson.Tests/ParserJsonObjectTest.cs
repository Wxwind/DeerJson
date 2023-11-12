namespace DeerJson.Tests;

public class ParserJsonObjectTest
{
    private readonly JsonMapper m_jsonMapper = new();

    [SetUp]
    public void Setup()
    {
    }

    [TestCase("SimpleObject.json")]
    public void Parser_Example1(string jsonName)
    {
        try
        {
            var filePath = Path.Combine("../../../TestJson", jsonName);
            var json = File.ReadAllText(filePath);
            var tree = m_jsonMapper.ParseToTree(json);
            Console.WriteLine(tree.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            Assert.Pass();
        }
    }
}