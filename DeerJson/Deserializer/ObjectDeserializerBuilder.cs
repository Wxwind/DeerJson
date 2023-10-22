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

        private readonly Dictionary<string, SettableProperty> m_propertyInfoDic =
            new Dictionary<string, SettableProperty>();


        public void SetType(Type type)
        {
            m_type = type;
        }

        public ObjectDeserializer Build()
        {
            var fis = m_type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            //find fields
            foreach (var pi in fis)
            {
                if (TypeUtil.IsAutoPropertyBackingField(pi))
                {
                    continue;
                }

                var settableProperty = new SettableProperty(pi);
                m_propertyInfoDic.Add(pi.Name, settableProperty);
            }

            // TODO: find auto prop
            var pis = m_type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            // foreach (var pi in pis)
            // {
            //     if (TypeUtil.IsAutoProperty(pi))
            //     {
            //         var settableProperty = new SettableProperty(pi);
            //         m_propertyInfoDic.Add(pi.Name, settableProperty);
            //     }
            // }

            return new ObjectDeserializer(m_type, m_propertyInfoDic);
        }
    }
}