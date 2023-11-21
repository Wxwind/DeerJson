using System;
using DeerJson.Deserializer.std;

namespace DeerJson.Deserializer
{
    public class DeserializeContext
    {
        private readonly DeserializerCache m_cache;
        private readonly JsonConfigure     m_configure;

        public DeserializeContext() : this(null)
        {
        }

        public DeserializeContext(JsonConfigure cfg)
        {
            m_cache = new DeserializerCache();
            m_configure = cfg;
        }

        private DeserializeContext(JsonConfigure cfg, DeserializerCache cache)
        {
            m_cache = cache;
            m_configure = cfg;
        }

        public bool IsEnabled(JsonFeature f)
        {
            return m_configure.IsEnabled(f);
        }

        public IKeyDeserializer FindStdKeySerializer(Type type)
        {
            return m_cache.FindStdKeySerializer(type);
        }

        public IDeserializer FindDeserializer(Type type)
        {
            return m_cache.FindOrCreateDeserializer(this, type);
        }

        public void AddCustomDeserializer<T>(JsonDeserializer<T> deserializer)
        {
            m_cache.AddCustomDeserializer(deserializer);
        }
    }
}