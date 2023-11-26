using System;
using System.Reflection;

namespace DeerJson.Serializer
{
    public class WritableProperty : IWritableMember
    {
        private          ISerializer  m_valueSerializer;
        private readonly PropertyInfo m_propInfo;
        public Type Type { get; }
        public string Name { get; }

        public WritableProperty(string name, PropertyInfo pi)
        {
            Name = name;
            m_propInfo = pi;
            Type = pi.PropertyType;
        }

        public void SetSerializer(ISerializer ser)
        {
            m_valueSerializer = ser;
        }

        public void SerializeAndWrite(JsonGenerator p, object obj, SerializeContext ctx)
        {
            p.WriteMemberName(Name);

            var value = m_propInfo.GetValue(obj);
            if (value == null)
            {
                p.WriteNull();
                return;
            }
            
            m_valueSerializer.Serialize(value, p, ctx);
        }
    }
}