
using DeerJson.Tests.Type;
using DeerJson.Tests.Util;
using FluentAssertions;

namespace DeerJson.Tests;

public class SerializeNullTest
{
    private readonly JsonMapper m_jsonMapper = new();

    [SetUp]
    public void Setup()
    {
        m_jsonMapper.Configure(JsonFeature.SERIALIZE_ORDER_BY_NAME, true);
    }

    [Test]
    public void NullObj()
    {
        var expected = ReadUtil.LoadJSON("NullObj.json").Replace("\n", "").Replace(" ", "").Replace("\t", "")
            .Replace("\r", "");


        var obj = new NullObj(null, null, '\0', null, 0);
        var json = m_jsonMapper.ToJson(obj);
        json.Should().Be(expected);
    }
}