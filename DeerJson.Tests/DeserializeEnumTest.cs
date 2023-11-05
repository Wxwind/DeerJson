using FluentAssertions;

namespace DeerJson.Tests;

public class DeserializeEnumTest
{
    private readonly JsonMapper m_jsonMapper = new();


    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void EnumObj1()
    {
        var json = """{"day":2}""";
        var obj = m_jsonMapper.ParseJson<DayObject>(json);
        var expected = new DayObject(Days.Tuesday);

        obj.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void EnumObj2()
    {
        var json = """{"day":"Tuesday"}""";
        var obj = m_jsonMapper.ParseJson<DayObject>(json);
        var expected = new DayObject(Days.Tuesday);

        obj.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void EnumObj3()
    {
        var json = """{"day":20000}""";

        var act = () => { m_jsonMapper.ParseJson<DayObject>(json); };

        act.Should().Throw<JsonException>().Where(e => e.Message.StartsWith("Enum"));
    }
}