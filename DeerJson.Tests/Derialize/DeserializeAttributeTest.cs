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
        // default configure are false,but just declare false explicitly here
        m_jsonMapper.Configure(JsonFeature.DESERIALIZE_FAIL_ON_TRAILING_TOKENS, false);
    }

    [Test]
    public void SimpleNestedObjectWithIgnore()
    {
        var json = ReadUtil.LoadJSON("SimpleNestedObject.json");
        var obj = JsonMapper.Default.ParseJson<SimpleNestedObjectWithIgnore>(json);
        var expected = new SimpleNestedObjectWithIgnore(null, new List<int> { 1, 2, 3 }, null);

        obj.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void SimpleNestedObjectTrailingToken()
    {
        var json = ReadUtil.LoadJSON("SimpleNestedObject.json") + """{"name":"123"}""";
        var obj = m_jsonMapper.ParseJson<SimpleNestedObject>(json);
        var subObj = new SubObject(1, true);
        var expected = new SimpleNestedObject("hello", new List<int> { 1, 2, 3 }, subObj);

        obj.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void SimpleNestedObjectFailInUnknownProperties()
    {
        m_jsonMapper.Configure(JsonFeature.DESERIALIZE_FAIL_ON_UNKNOWN_PROPERTIES, true);
        var json = """
                   {
                     "numArr": [
                       1,
                       2,
                       3
                     ],
                     "what is this": "???",
                     "str": "hello",
                     "subObj": {
                       "isObj": true,
                       "subNum": 1
                     }
                   }
                   """;


        var act = () => { m_jsonMapper.ParseJson<SimpleNestedObject>(json); };

        act.Should().Throw<JsonException>().Where(e =>
            e.Message == "serializing SimpleNestedObject: missing field 'what is this'.");
    }

    [Test]
    public void SimpleNestedObjectNotFailedInUnknownProperties()
    {
        m_jsonMapper.Configure(JsonFeature.DESERIALIZE_FAIL_ON_UNKNOWN_PROPERTIES, false);
        var json = """
                   {
                     "numArr": [
                       1,
                       2,
                       3
                     ],
                     "what is this": "???",
                     "str": "hello",
                     "subObj": {
                       "isObj": true,
                       "subNum": 1
                     }
                   }
                   """;
        var obj = m_jsonMapper.ParseJson<SimpleNestedObject>(json);
        var subObj = new SubObject(1, true);
        var expected = new SimpleNestedObject("hello", new List<int> { 1, 2, 3 }, subObj);

        obj.Should().BeEquivalentTo(expected);
    }
}