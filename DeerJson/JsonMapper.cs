using System;
using DeerJson.Deserializer;
using DeerJson.Node;
using DeerJson.Serializer;

namespace DeerJson
{
    public class JsonMapper
    {
        private readonly DeserializeContext m_deserializeContext = new DeserializeContext();
        private readonly SerializeContext   m_serializeContext   = new SerializeContext();

        public JsonMapper()
        {
        }


        public T ParseJson<T>(string json)
        {
            return (T)ParseJson(typeof(T), json);
        }

        private object ParseJson(Type type, string json)
        {
            var dese = m_deserializeContext.FindDeserializer(type);
            var p = new JsonParser(json);
            return dese.Deserialize(p);
        }

        // TODO: Extend feature of JsonObject
        public JsonNode ParseToTree(string json)
        {
            return ParseJson<JsonNode>(json);
        }

        public string ToJson(object value)
        {
            using (var gen = new JsonGenerator())
            {
                var ser = m_serializeContext.FindSerializer(value.GetType());
                ser.Serialize(value, gen);
                return gen.GetValueAsString();
            }
        }
        
    }
}