using DeerJson.Tests.TestJson;
using FluentAssertions;

namespace DeerJson.Tests;

public class DeserializeTest
{
    private readonly JsonMapper m_jsonMapper = new();


    [SetUp]
    public void Setup()
    {
    }

    [TestCase("PlainObj.json")]
    public void Dese_PlainObj(string jsonName)
    {
        var filePath = Path.Combine("../../../TestJson", jsonName);
        var json = File.ReadAllText(filePath);
        var obj = m_jsonMapper.ParseJson<PlainObj>(json);
        var expected = new PlainObj("wxwind", true, 123, 'h');

        obj.Should().BeEquivalentTo(expected);

        //Assert.That(obj, Is.EqualTo(expected));
    }
}