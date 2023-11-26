using System;
using System.Reflection;

namespace DeerJson.Serializer
{
    public class WritableField : IWritableMember
    {
        private          ISerializer m_valueSerializer;
        private readonly FieldInfo   m_fieldInfo;

        public Type Type { get; }
        public string Name { get; }

        public WritableField(string name, FieldInfo pi)
        {
            Name = name;
            m_fieldInfo = pi;
            Type = pi.FieldType;
        }

        public void SetSerializer(ISerializer ser)
        {
            m_valueSerializer = ser;
        }

        public void SerializeAndWrite(JsonGenerator p, object obj, SerializeContext ctx)
        {
            p.WriteMemberName(Name);
            
            var value = m_fieldInfo.GetValue(obj);
            if (value == null)
            {
                p.WriteNull();
                return;
            }
            
            m_valueSerializer.Serialize(value, p, ctx);
        }
    }
}