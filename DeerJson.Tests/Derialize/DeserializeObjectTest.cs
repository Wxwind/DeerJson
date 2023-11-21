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

    [Test]
    public void PlainObj()
    {
        var json = ReadUtil.LoadJSON("PlainObj.json");
        var obj = m_jsonMapper.ParseJson<PlainObj>(json);
        var expected = new PlainObj("wxwind", true, 123, 'h');

        obj.Should().BeEquivalentTo(expected);

        //Assert.That(obj, Is.EqualTo(expected));
    }

    [Test]
    public void PlainObjWithAutoProp()
    {
        var json = ReadUtil.LoadJSON("PlainObj.json");
        var obj = m_jsonMapper.ParseJson<PlainObjWithAutoProp>(json);
        var expected = new PlainObjWithAutoProp("wxwind", true, 123, 'h');

        obj.Should().BeEquivalentTo(expected);

        //Assert.That(obj, Is.EqualTo(expected));
    }

    [Test]
    public void SimpleNestedObject()
    {
        var json = ReadUtil.LoadJSON("SimpleNestedObject.json");
        var obj = m_jsonMapper.ParseJson<SimpleNestedObject>(json);
        var subObj = new SubObject(1, true);
        var expected = new SimpleNestedObject("hello", new List<int> { 1, 2, 3 }, subObj);

        obj.Should().BeEquivalentTo(expected);
    }
}