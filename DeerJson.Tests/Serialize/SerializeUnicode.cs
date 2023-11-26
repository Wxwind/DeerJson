using FluentAssertions;

namespace DeerJson.Tests;

public class SerializeUnicode
{
    private readonly JsonMapper m_jsonMapper = new();


    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Unicode()
    {
        var expected = @"""°""";
        var s = '°';
        var json = m_jsonMapper.ToJson(s);

        json.Should().Be(expected);
    }
}