using System;
using System.Reflection;
using DeerJson.Deserializer.std;

namespace DeerJson.Deserializer
{
    public class SettableField : ISettableMember
    {
        private          IDeserializer m_valueDeserializer;
        private readonly FieldInfo     m_fieldInfo;
        public Type Type { get; }


        public SettableField(FieldInfo pi)
        {
            m_fieldInfo = pi;
            Type = pi.FieldType;
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