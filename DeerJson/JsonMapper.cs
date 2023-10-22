using System;
using DeerJson.Deserializer;
using DeerJson.Deserializer.std;
using DeerJson.Node;

namespace DeerJson
{
    public class JsonMapper
    {
        private readonly DeserializeContext m_deserializeContext = new DeserializeContext();

        public JsonMapper()
        {
        }


        public T ParseJson<T>(string json)
        {
            return (T)ParseJson(typeof(T), json);
        }

        private object ParseJson(Type type, string json)
        {
            var desc = m_deserializeContext.FindDeserializer(type);
            var p = new JsonParser(json);
            return ReadValue(p, desc);
        }

        public JsonNode ParseToTree(string json)
        {
            return ParseJson<JsonNode>(json);
        }

        public string ToJson<T>(T value)
        {
            return "";
        }

        private object ReadValue(JsonParser p, IDeserializer dese)
        {
            return dese.Deserialize(p);
        }
    }
}