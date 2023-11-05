using System;
using System.Reflection;

namespace DeerJson.Serializer
{
    public class WritableField : IWritableMember
    {
        private          ISerializer m_valueSerializer;
        private readonly FieldInfo   m_fieldInfo;
        private readonly string      m_name;
        public Type Type { get; }


        public WritableField(string name, FieldInfo pi)
        {
            m_name = name;
            m_fieldInfo = pi;
            Type = pi.FieldType;
        }

        public void SetSerializer(ISerializer ser)
        {
            m_valueSerializer = ser;
        }

        public void SerializeAndWrite(JsonGenerator p, object obj)
        {
            var value = m_fieldInfo.GetValue(obj);
            p.WriteMemberName(m_name);
            m_valueSerializer.Serialize(value, p);
        }
    }
}