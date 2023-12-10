
using DeerJson.Tests.Type;
using DeerJson.Tests.Util;
using FluentAssertions;

namespace DeerJson.Tests;

public class SerializeNullTest
{
    private readonly JsonMapper m_jsonMapper = new();

    [SetUp]
    public void Setup()
    {
        m_jsonMapper.Configure(JsonFeature.SERIALIZE_ORDER_BY_NAME, true);
    }

    [Test]
    public void NullObj()
    {
        var expected = ReadUtil.LoadJSON("NullObj.json").Replace("\n", "").Replace(" ", "").Replace("\t", "")
            .Replace("\r", "");


        var obj = new NullObj(null, null, '\0', null, 0);
        var json = m_jsonMapper.ToJson(obj);
        json.Should().Be(expected);
    }

    [Test]
    public void NullArray()
    {
        var expected = """["a",null,"b"]""";

        var obj = new string[] { "a", null, "b" };
        var json = m_jsonMapper.ToJson(obj);
        json.Should().Be(expected);
    }

    [Test]
    public void NullList()
    {
        var expected = """["a",null,"b"]""";

        var obj = new List<string> { "a", null, "b" };
        var json = m_jsonMapper.ToJson(obj);
        json.Should().Be(expected);
    }

    [Test]
    public void NullObjInArray()
    {
        var expected = ReadUtil.LoadJSON("SimpleNestedObjectArrayWithNull.json").Replace("\n", "").Replace(" ", "")
            .Replace("\t", "")
            .Replace("\r", "");

        var a = new List<int> { 1, 2, 3 };
        var subObj = new SubObject(1, true);
        var obj = new SimpleNestedObject("hello", a, subObj);

        var arr = new SimpleNestedObject[] { obj, null, obj };
        var json = m_jsonMapper.ToJson(arr);
        json.Should().Be(expected);
    }
}