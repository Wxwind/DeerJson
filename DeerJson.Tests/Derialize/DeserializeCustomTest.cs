using DeerJson.Tests.Type;
using DeerJson.Tests.Util;
using FluentAssertions;

namespace DeerJson.Tests;

public class DeserializeCustomTest
{
    private JsonMapper m_jsonMapper;

    [SetUp]
    public void Setup()
    {
        m_jsonMapper = new JsonMapper();
        m_jsonMapper.AddDeserializer(new SimpleNestedObjectDeserializer());
    }

    [Test]
    public void CustomDeserializer()
    {
        var json = ReadUtil.LoadJSON("SimpleNestedObject.json");
        var obj = m_jsonMapper.ParseJson<SimpleNestedObject>(json);
        var subObj = new SubObject(1, true);
        var expected = new SimpleNestedObject("hi", new List<int> { 1, 2, 3 }, subObj);

        obj.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void CustomDeserializerWithNull()
    {
        var json = ReadUtil.LoadJSON("SimpleNestedObjectWithNull.json");
        var obj = m_jsonMapper.ParseJson<SimpleNestedObject>(json);
        var subObj = new SubObject(0, true);
        var expected = new SimpleNestedObject("hi", null, subObj);

        obj.Should().BeEquivalentTo(expected);
    }
}