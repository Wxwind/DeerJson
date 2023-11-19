using DeerJson.Tests.Util;
using FluentAssertions;

namespace DeerJson.Tests;

public class DeserializeCollectionTest
{
    private readonly JsonMapper m_jsonMapper = new();


    [SetUp]
    public void Setup()
    {
    }

    [TestCase("IntArray.json")]
    public void IntArray(string jsonName)
    {
        var json = ReadUtil.LoadJSON(jsonName);
        var obj = m_jsonMapper.ParseJson<int[]>(json);
        var expected = new[] { 1, 2, 3 };

        obj.Should().BeEquivalentTo(expected);
    }


    [TestCase("IntArray.json")]
    public void IntList(string jsonName)
    {
        var json = ReadUtil.LoadJSON(jsonName);
        var obj = m_jsonMapper.ParseJson<List<int>>(json);
        var expected = new List<int> { 1, 2, 3 };

        obj.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Dictionary1()
    {
        var json = """{"a":1,"b":2}""";
        var obj = m_jsonMapper.ParseJson<Dictionary<string, int>>(json);
        var expected = new Dictionary<string, int> { { "a", 1 }, { "b", 2 } };

        obj.Should().BeEquivalentTo(expected);
    }
}