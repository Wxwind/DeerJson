using DeerJson.Tests.Type;
using FluentAssertions;

namespace DeerJson.Tests;

public class SerializeEnumTest
{
    private readonly JsonMapper m_jsonMapper  = new();
    private readonly JsonMapper m_jsonMapper2 = new();


    [SetUp]
    public void Setup()
    {
        m_jsonMapper2.Enable(JsonFeature.SERIALIZE_UNDERLYING_TYPE_FOR_ENUM);
    }

    [Test]
    public void EnumWithUnderlyingType()
    {
        var expected = "2";
        var json = m_jsonMapper2.ToJson(Days.Tuesday);

        json.Should().Be(expected);
    }

    [Test]
    public void EnumWithStr()
    {
        var expected = """
                       "Tuesday"
                       """;
        var json = m_jsonMapper.ToJson(Days.Tuesday);

        json.Should().Be(expected);
    }

    [Test]
    public void EnumObjWithUnderlyingType()
    {
        var expected = """{"day":2}""";

        var obj = new DayObject(Days.Tuesday);
        var json = m_jsonMapper2.ToJson(obj);

        json.Should().Be(expected);
    }

    [Test]
    public void EnumObjWithStr()
    {
        var expected = """{"day":"Tuesday"}""";

        var obj = new DayObject(Days.Tuesday);
        var json = m_jsonMapper.ToJson(obj);

        json.Should().Be(expected);
    }
}