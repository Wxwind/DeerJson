using DeerJson.Tests.Type;
using DeerJson.Tests.Util;
using FluentAssertions;

namespace DeerJson.Tests;

public class SerializeAttributeTest
{
    private readonly JsonMapper m_jsonMapper = new();

    [SetUp]
    public void Setup()
    {
        m_jsonMapper.Enable(JsonFeature.SERIALIZE_UNDERLYING_TYPE_FOR_ENUM);
    }

    [Test]
    public void SimpleNestedObjectWithIgnore()
    {
        var expected = ReadUtil.LoadJSON("SimpleNestedObjectWithIgnore.json").Replace("\n", "").Replace(" ", "")
            .Replace("\t", "")
            .Replace("\r", "");
        var subObj = new SubObject(1, true);
        var obj = new SimpleNestedObjectWithIgnore("hello", new List<int> { 1, 2, 3 }, subObj);
        var json = m_jsonMapper.ToJson(obj);

        json.Should().Be(expected);
    }

}