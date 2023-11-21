using DeerJson.Tests.Type;
using FluentAssertions;

namespace DeerJson.Tests;

public class SerializeCustomTest
{
    private readonly JsonMapper m_jsonMapper = new();

    [SetUp]
    public void Setup()
    {
        m_jsonMapper.AddSerializer(new SimpleNestedObjectSerializer());
    }

    [Test]
    public void CustomSerializer()
    {
        var expected = """
                       {"str2":"hello","arr2":[1,2,3]}
                       """;
        var a = new List<int> { 1, 2, 3 };
        var subObj = new SubObject(1, true);
        var obj = new SimpleNestedObject("hello", a, subObj);
        var json = m_jsonMapper.ToJson(obj);

        json.Should().Be(expected);
    }
}