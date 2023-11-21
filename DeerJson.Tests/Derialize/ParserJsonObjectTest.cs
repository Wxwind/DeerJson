using DeerJson.Tests.Util;

namespace DeerJson.Tests;

public class ParserJsonObjectTest
{
    private readonly JsonMapper m_jsonMapper = new();

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Parser_Example1()
    {
        try
        {
            var json = ReadUtil.LoadJSON("SimpleObject.json");
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