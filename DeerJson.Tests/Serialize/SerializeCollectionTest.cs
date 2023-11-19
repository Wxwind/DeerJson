using FluentAssertions;

namespace DeerJson.Tests;

public class SerializeCollectionTest
{
    private readonly JsonMapper m_jsonMapper = new();


    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void List1()
    {
        var expected = "[1,2,3]";
        var list = new List<int> { 1, 2, 3 };
        var json = m_jsonMapper.ToJson(list);

        json.Should().Be(expected);
    }

    [Test]
    public void Array1()
    {
        var expected = "[1,2,3]";
        var list = new[] { 1, 2, 3 };
        var json = m_jsonMapper.ToJson(list);

        json.Should().Be(expected);
    }

    [Test]
    public void Dictionary1()
    {
        var expected = """{"a":1,"b":2}""";
        var dic = new Dictionary<string, int> { { "a", 1 }, { "b", 2 } };
        var json = m_jsonMapper.ToJson(dic);

        json.Should().Be(expected);
    }
}