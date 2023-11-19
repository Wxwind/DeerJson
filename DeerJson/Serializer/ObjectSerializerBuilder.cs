using System;
using System.Collections.Generic;
using System.Reflection;
using DeerJson.Serializer.std;
using DeerJson.Util;

namespace DeerJson.Serializer
{
    public class ObjectSerializerBuilder
    {
        private Type m_type;

        private readonly List<IWritableMember> m_memberInfoList =
            new List<IWritableMember>();


        public void SetType(Type type)
        {
            m_type = type;
        }

        public ObjectSerializer Build()
        {
            var fis = m_type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            //find fields
            foreach (var fi in fis)
            {
                if (TypeUtil.IsIgnoreMember(fi))
                {
                    continue;
                }
                
                if (TypeUtil.IsAutoPropertyBackingField(fi))
                {
                    continue;
                }

                var writableField = new WritableField(fi.Name, fi);
                m_memberInfoList.Add(writableField);
            }
            
            var pis = m_type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var pi in pis)
            {
                if (TypeUtil.IsIgnoreMember(pi))
                {
                    continue;
                }
                
                if (TypeUtil.IsAutoProperty(pi))
                {
                    var writableProperty = new WritableProperty(pi.Name, pi);
                    m_memberInfoList.Add(writableProperty);
                }
            }

            return new ObjectSerializer(m_type, m_memberInfoList);
        }
    }
}