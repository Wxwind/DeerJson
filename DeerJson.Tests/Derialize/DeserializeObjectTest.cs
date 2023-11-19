using DeerJson.Tests.Type;
using DeerJson.Tests.Util;
using FluentAssertions;

namespace DeerJson.Tests;

public class DeserializeObjectTest
{
    private readonly JsonMapper m_jsonMapper = new();
    
    [SetUp]
    public void Setup()
    {
    }

    [TestCase("PlainObj.json")]
    public void PlainObj(string jsonName)
    {
        var json = ReadUtil.LoadJSON(jsonName);
        var obj = m_jsonMapper.ParseJson<PlainObj>(json);
        var expected = new PlainObj("wxwind", true, 123, 'h');

        obj.Should().BeEquivalentTo(expected);

        //Assert.That(obj, Is.EqualTo(expected));
    }

    [TestCase("PlainObj.json")]
    public void PlainObjWithAutoProp(string jsonName)
    {
        var json = ReadUtil.LoadJSON(jsonName);
        var obj = m_jsonMapper.ParseJson<PlainObjWithAutoProp>(json);
        var expected = new PlainObjWithAutoProp("wxwind", true, 123, 'h');

        obj.Should().BeEquivalentTo(expected);

        //Assert.That(obj, Is.EqualTo(expected));
    }

    [TestCase("SimpleNestedObject.json")]
    public void SimpleNestedObject(string jsonName)
    {
        var json = ReadUtil.LoadJSON(jsonName);
        var obj = m_jsonMapper.ParseJson<SimpleNestedObject>(json);
        var subObj = new SubObject(1, true);
        var expected = new SimpleNestedObject("hello", new List<int> { 1, 2, 3 }, subObj);

        obj.Should().BeEquivalentTo(expected);
    }


    
}