using System;

namespace DeerJson.Serializer
{
    public class SerializeContext
    {
        private readonly SerializerCache m_cache = new SerializerCache();
        private readonly JsonConfigure   m_configure;

        public SerializeContext() : this(null)
        {
        }

        public SerializeContext(JsonConfigure cfg)
        {
            m_cache = new SerializerCache();
            m_configure = cfg;
        }

        public SerializeContext CreateInstance(JsonConfigure cfg)
        {
            return new SerializeContext(cfg);
        }

        public bool IsEnabled(JsonFeature f)
        {
            return m_configure.IsEnabled(f);
        }


        public ISerializer FindSerializer(Type type)
        {
            return m_cache.FindOrCreateSerializer(this, type);
        }
    }
}