using DeerJson.Tests.Type;
using DeerJson.Tests.Util;
using FluentAssertions;

namespace DeerJson.Tests;

public class DeserializeNullTest
{
    private readonly JsonMapper m_jsonMapper = new();

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void NullObjWithAllNull()
    {
        m_jsonMapper.Configure(JsonFeature.DESERIALIZE_FAIL_ON_NULL_FOR_PRIMITIVES, false);
        var json = ReadUtil.LoadJSON("NullObjWithAllNull.json");
        var obj = m_jsonMapper.ParseJson<NullObj>(json);
        var expected = new NullObj();

        obj.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void NullObj()
    {
        m_jsonMapper.Configure(JsonFeature.DESERIALIZE_FAIL_ON_NULL_FOR_PRIMITIVES, true);
        var json = ReadUtil.LoadJSON("NullObj.json");
        var obj = m_jsonMapper.ParseJson<NullObj>(json);
        var expected = new NullObj();

        obj.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void NullSimpleNestedObj()
    {
        var json = ReadUtil.LoadJSON("SimpleNestedObjectWithNull.json");
        var obj = m_jsonMapper.ParseJson<SimpleNestedObject>(json);
        var subObj = new SubObject(0, true);
        var expected = new SimpleNestedObject("hello", null, subObj);

        obj.Should().BeEquivalentTo(expected);
    }
}