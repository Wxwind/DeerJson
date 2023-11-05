using System;

namespace DeerJson.Serializer
{
    public class SerializeContext
    {
        private readonly SerializerCache m_cache = new SerializerCache();

        public ISerializer FindSerializer(Type type)
        {
            return m_cache.FindOrCreateSerializer(this, type);
        }
    }
}