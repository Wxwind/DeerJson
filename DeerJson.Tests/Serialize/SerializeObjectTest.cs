using DeerJson.Tests.Type;
using DeerJson.Tests.Util;
using FluentAssertions;

namespace DeerJson.Tests;

public class SerializeObjectTest
{
    private readonly JsonMapper m_jsonMapper = new();


    [SetUp]
    public void Setup()
    {
        m_jsonMapper.Configure(JsonFeature.SERIALIZE_ORDER_BY_NAME, true);
    }

    [TestCase("PlainObj.json")]
    public void PlainObj(string jsonName)
    {
        var expected = ReadUtil.LoadJSON(jsonName).Replace("\n", "").Replace(" ", "").Replace("\t", "")
            .Replace("\r", "");


        var obj = new PlainObj("wxwind", true, 123, 'h');
        var json = m_jsonMapper.ToJson(obj);

        json.Should().Be(expected);
    }

    [Test]
    public void PlainObjWithAutoProp()
    {
        var expected = ReadUtil.LoadJSON("PlainObj.json").Replace("\n", "").Replace(" ", "").Replace("\t", "")
            .Replace("\r", "");


        var obj = new PlainObjWithAutoProp("wxwind", true, 123, 'h');
        var json = m_jsonMapper.ToJson(obj);

        json.Should().Be(expected);
    }

    [Test]
    public void SimpleNestedObject()
    {
        var expected = ReadUtil.LoadJSON("SimpleNestedObject.json").Replace("\n", "").Replace(" ", "")
            .Replace("\t", "")
            .Replace("\r", "");

        var a = new List<int> { 1, 2, 3 };
        var subObj = new SubObject(1, true);
        var obj = new SimpleNestedObject("hello", a, subObj);
        var json = m_jsonMapper.ToJson(obj);

        json.Should().Be(expected);
    }
}