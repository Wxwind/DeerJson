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

        private readonly List<WritableProperty> m_memberInfoList =
            new List<WritableProperty>();


        public void SetType(Type type)
        {
            m_type = type;
        }

        public ObjectSerializer Build()
        {
            var fis = m_type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            //find fields
            foreach (var pi in fis)
            {
                if (TypeUtil.IsAutoPropertyBackingField(pi))
                {
                    continue;
                }

                var writableProperty = new WritableProperty(pi.Name, pi);
                m_memberInfoList.Add(writableProperty);
            }

            // TODO: find auto prop
            var pis = m_type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            // foreach (var pi in pis)
            // {
            //     if (TypeUtil.IsAutoProperty(pi))
            //     {
            //         var WritableProperty = new WritableProperty(pi);
            //         m_memberInfoList.Add(pi.Name, WritableProperty);
            //     }
            // }

            return new ObjectSerializer(m_type, m_memberInfoList);
        }
    }
}