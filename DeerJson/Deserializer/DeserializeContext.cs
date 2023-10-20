using System;
using DeerJson.Deserializer.std;

namespace DeerJson.Deserializer
{
    public class DeserializeContext
    {
        private DeserializerCache m_cache = new DeserializerCache();

        public DeserializeContext()
        {
        }

        public IDeserializer FindDeseializer(Type type)
        {
            return m_cache.FindOrCreateDeserializer(this, type);
        }
    }
}