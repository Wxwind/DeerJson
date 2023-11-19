using System;
using System.Collections.Generic;
using System.Reflection;
using DeerJson.Deserializer.std;
using DeerJson.Util;

namespace DeerJson.Deserializer
{
    public class ObjectDeserializerBuilder
    {
        private Type m_type;

        private readonly Dictionary<string, ISettableMember> m_memberInfoDic =
            new Dictionary<string, ISettableMember>();


        public void SetType(Type type)
        {
            m_type = type;
        }

        public ObjectDeserializer Build()
        {
            var ignoreNames = new List<string>();
            
            var fis = m_type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            //find fields
            foreach (var fi in fis)
            {
                if (TypeUtil.IsIgnoreMember(fi))
                {
                    ignoreNames.Add(fi.Name);
                    continue;
                }
                
                if (TypeUtil.IsAutoPropertyBackingField(fi))
                {
                    continue;
                }

                var settableProperty = new SettableField(fi);
                m_memberInfoDic.Add(fi.Name, settableProperty);
            }
            
            var pis = m_type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var pi in pis)
            {
                if (TypeUtil.IsIgnoreMember(pi))
                {
                    ignoreNames.Add(pi.Name);
                    continue;
                }
                
                if (TypeUtil.IsAutoProperty(pi))
                {
                    var settableProperty = new SettableProperty(pi);
                    m_memberInfoDic.Add(pi.Name, settableProperty);
                }
            }

            return new ObjectDeserializer(m_type, m_memberInfoDic, ignoreNames);
        }

    }
}