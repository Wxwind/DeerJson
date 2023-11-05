﻿using System;
using System.Reflection;

namespace DeerJson.Serializer
{
    public class WritableProperty : IWritableMember
    {
        private          ISerializer  m_valueSerializer;
        private readonly PropertyInfo m_propInfo;
        private readonly string       m_name;
        public Type Type { get; }


        public WritableProperty(string name, PropertyInfo pi)
        {
            m_name = name;
            m_propInfo = pi;
            Type = pi.PropertyType;
        }

        public void SetSerializer(ISerializer ser)
        {
            m_valueSerializer = ser;
        }

        public void SerializeAndWrite(JsonGenerator p, object obj)
        {
            var value = m_propInfo.GetValue(obj);
            p.WriteMemberName(m_name);
            m_valueSerializer.Serialize(value, p);
        }
    }
}