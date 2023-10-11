using System;
using System.Collections.Generic;
using DeerJson.AST;
using DeerJson.Deserializer;

namespace DeerJson
{
    public class JsonMapper
    {
        private Dictionary<Type, JsonDeserializer<object>> m_deserializerMap =
            new Dictionary<Type, JsonDeserializer<object>>();

        public T ParseJson<T>(string json)
        {
            return (T)ParseJson(typeof(T), json);
        }

        public object ParseJson(Type type, string json)
        {
            var obj = Activator.CreateInstance(type);
            var parser = new JsonParser(json);
            // Todo
            return null;
        }

        public JsonNode ParseToTree(string json)
        {
            var parser = new JsonParser(json);
            return parser.Parse(json);
        }
    }
}