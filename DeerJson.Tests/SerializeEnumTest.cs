using FluentAssertions;

namespace DeerJson.Tests;

public class SerializeEnumTest
{
    private readonly JsonMapper m_jsonMapper = new();


    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Enum1()
    {
        var expected = "2";
        var json = m_jsonMapper.ToJson(Days.Tuesday);

        json.Should().Be(expected);
    }

    // [Test]
    // public void Enum2()
    // {
    //     var expected = """
    //                    "Tuesday"
    //                    """;
    //     var json = m_jsonMapper.ToJson(Days.Tuesday);
    //
    //     json.Should().Be(expected);
    // }

    [Test]
    public void EnumObj1()
    {
        var expected = """{"day":2}""";

        var obj = new DayObject(Days.Tuesday);
        var json = m_jsonMapper.ToJson(obj);

        json.Should().Be(expected);
    }
}