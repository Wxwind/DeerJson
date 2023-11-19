using DeerJson.Tests.Type;
using DeerJson.Tests.Util;
using FluentAssertions;

namespace DeerJson.Tests;

public class DeserializeAttributeTest
{
    private readonly JsonMapper m_jsonMapper = new();

    [SetUp]
    public void Setup()
    {
    }

    [TestCase("SimpleNestedObject.json")]
    public void SimpleNestedObjectWithIgnore(string jsonName)
    {
        var json = ReadUtil.LoadJSON(jsonName);
        var obj = m_jsonMapper.ParseJson<SimpleNestedObjectWithIgnore>(json);
        var expected = new SimpleNestedObjectWithIgnore(null, new List<int> { 1, 2, 3 }, null);

        obj.Should().BeEquivalentTo(expected);
    }
}