using System;
using System.Reflection;
using DeerJson.Deserializer.std;

namespace DeerJson.Deserializer
{
    public class SettableProperty
    {
        private          IDeserializer m_valueDeserializer;
        private readonly FieldInfo     m_fieldInfo;
        private readonly Type          m_type;
        public Type Type => m_type;


        public SettableProperty(FieldInfo pi)
        {
            m_fieldInfo = pi;
            m_type = pi.FieldType;
        }

        public void SetDeserializer(IDeserializer deser)
        {
            m_valueDeserializer = deser;
        }

        public void DeserializeAndSet(JsonParser p, object obj)
        {
            var v = m_valueDeserializer.Deserialize(p);
            m_fieldInfo.SetValue(obj, v);
        }
    }
}