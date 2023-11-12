using System;
using System.Reflection;
using DeerJson.Deserializer.std;

namespace DeerJson.Deserializer
{
    public class SettableProperty : ISettableMember
    {
        private          IDeserializer m_valueDeserializer;
        private readonly PropertyInfo  m_propInfo;
        public Type Type { get; }


        public SettableProperty(PropertyInfo pi)
        {
            m_propInfo = pi;
            Type = pi.PropertyType;
        }

        public void SetDeserializer(IDeserializer deser)
        {
            m_valueDeserializer = deser;
        }

        public void DeserializeAndSet(JsonParser p, object obj, DeserializeContext ctx)
        {
            var v = m_valueDeserializer.Deserialize(p, ctx);
            m_propInfo.SetValue(obj, v);
        }
    }
}