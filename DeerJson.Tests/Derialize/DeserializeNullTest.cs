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
    public void Null1()
    {
        var json = "null";
        var obj = m_jsonMapper.ParseJson<NullObj>(json);
        NullObj expected = null;

        obj.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Null2()
    {
        var json = "null";
        var obj = m_jsonMapper.ParseJson<int>(json);
        var expected = 0;

        obj.Should().Be(expected);
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

    [Test]
    public void NullArray()
    {
        var json = """["a",null,"b"]""";
        var arr = m_jsonMapper.ParseJson<string[]>(json);

        var expected = new string[] { "a", null, "b" };

        arr.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void NullList()
    {
        var json = """["a",null,"b"]""";
        var arr = m_jsonMapper.ParseJson<List<string>>(json);

        var expected = new string[] { "a", null, "b" };

        arr.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void NullObjInArray()
    {
        var json = ReadUtil.LoadJSON("SimpleNestedObjectArrayWithNull.json").Replace("\n", "").Replace(" ", "");

        var arr = m_jsonMapper.ParseJson<List<SimpleNestedObject>>(json);

        var subObj = new SubObject(1, true);
        var obj = new SimpleNestedObject("hello", new List<int> { 1, 2, 3 }, subObj);
        var expected = new SimpleNestedObject[] { obj, null, obj };

        arr.Should().BeEquivalentTo(expected);
    }
}