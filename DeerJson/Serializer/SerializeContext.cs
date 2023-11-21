using System;

namespace DeerJson.Serializer
{
    public class SerializeContext
    {
        private readonly SerializerCache m_cache;
        private readonly JsonConfigure   m_configure;

        public SerializeContext() : this(null, null)
        {
        }

        public SerializeContext(JsonConfigure cfg)
        {
            m_cache = new SerializerCache();
            m_configure = cfg;
        }

        private SerializeContext(JsonConfigure cfg, SerializerCache cache)
        {
            m_cache = cache;
            m_configure = cfg;
        }
        

        public bool IsEnabled(JsonFeature f)
        {
            return m_configure.IsEnabled(f);
        }


        public ISerializer FindSerializer(Type type)
        {
            return m_cache.FindOrCreateSerializer(this, type);
        }

        public ISerializer FindStdKeySerializer(Type type)
        {
            return m_cache.FindStdKeySerializer(type);
        }

        public void AddCustomSerializer<T>(JsonSerializer<T> serializer)
        {
            m_cache.AddCustomSerializer(serializer);
        }
    }
}