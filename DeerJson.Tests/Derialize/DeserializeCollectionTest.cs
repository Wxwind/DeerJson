using DeerJson.Tests.Type;
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

    [Test]
    public void IntArray()
    {
        var json = ReadUtil.LoadJSON("IntArray.json");
        var obj = m_jsonMapper.ParseJson<int[]>(json);
        var expected = new[] { 1, 2, 3 };

        obj.Should().BeEquivalentTo(expected);
    }


    [Test]
    public void IntList()
    {
        var json = ReadUtil.LoadJSON("IntArray.json");
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

    [Test]
    public void DictionaryEnumKeyByName()
    {
        var json = """{"Tuesday":1,"Friday":2}""";
        var obj = m_jsonMapper.ParseJson<Dictionary<Days, int>>(json);
        var expected = new Dictionary<Days, int> { { Days.Tuesday, 1 }, { Days.Friday, 2 } };

        obj.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void DictionaryEnumKeyByBaseValue()
    {
        var json = """{"2":1,"5":2}""";
        var obj = m_jsonMapper.ParseJson<Dictionary<Days, int>>(json);
        var expected = new Dictionary<Days, int> { { Days.Tuesday, 1 }, { Days.Friday, 2 } };

        obj.Should().BeEquivalentTo(expected);
    }
}