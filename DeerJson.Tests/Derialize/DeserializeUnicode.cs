using FluentAssertions;

namespace DeerJson.Tests;

public class DeserializeUnicode
{
    private readonly JsonMapper m_jsonMapper = new();

    [SetUp]
    public void Setup()
    {
    }

    // [Test]
    // public void Unicode()
    // {
    //     var json = "\"\u0080\"";
    //     var obj = m_jsonMapper.ParseJson<string>(json);
    //     var expected = "°";
    //
    //     obj.Should().BeEquivalentTo(expected);
    // }

    [Test]
    public void Unicode()
    {
        var json = "\"°\"";
        var obj = m_jsonMapper.ParseJson<string>(json);
        var expected = "°";

        obj.Should().BeEquivalentTo(expected);
    }
}