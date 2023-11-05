﻿using DeerJson.Tests.TestJson;
using DeerJson.Tests.Util;
using FluentAssertions;

namespace DeerJson.Tests;

public class SerializeTest
{
    private readonly JsonMapper m_jsonMapper = new();


    [SetUp]
    public void Setup()
    {
    }

    [TestCase("PlainObj.json")]
    public void PlainObj(string jsonName)
    {
        var expected = ReadUtil.LoadJSON(jsonName).Replace("\n", "").Replace(" ", "").Replace("\t", "")
            .Replace("\r", "");


        var obj = new PlainObj("wxwind", true, 123, 'h');
        var json = m_jsonMapper.ToJson(obj);

        json.Should().Be(expected);
    }
}