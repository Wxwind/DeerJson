using System;
using System.Collections.Generic;

namespace DeerJson.Deserializer.std
{
    public class ObjectDeserializer : JsonDeserializer<object>, IResolvableDeserializer
    {
        private readonly Type                                m_type;
        private readonly Dictionary<string, ISettableMember> m_memberInfoDic;
        private readonly List<string>                        m_ignoreNameList;


        public ObjectDeserializer(Type classType, Dictionary<string, ISettableMember> memberInfoDic,
            List<string> ignoreNames)
        {
            m_type = classType;
            m_memberInfoDic = memberInfoDic;
            m_ignoreNameList = ignoreNames;
        }

        public override object Deserialize(JsonParser p, DeserializeContext ctx)
        {
            p.GetObjectStart();
            var o = Activator.CreateInstance(m_type);

            string name;

            while ((name = p.GetMemberName()) != null)
            {
                if (m_memberInfoDic.TryGetValue(name, out var settableMember))
                {
                    settableMember.DeserializeAndSet(p, o, ctx);
                }
                else
                {
                    if (m_ignoreNameList.Contains(name))
                    {
                        p.SkipMemberValue();
                        continue;
                    }

                    if (ctx.IsEnabled(JsonFeature.DESERIALIZE_FAIL_ON_UNKNOWN_PROPERTIES))
                    {
                        throw new JsonException($"serializing {m_type.Name}: missing field '{name}'.");
                    }
                    else p.SkipMemberValue();
                }
            }

            p.GetObjectEnd();
            return o;
        }

        public void Resolve(DeserializeContext ctx)
        {
            foreach (var sp in m_memberInfoDic.Values)
            {
                var desc = ctx.FindDeserializer(sp.Type);
                sp.SetDeserializer(desc);
            }
        }

        public override object GetNullValue(DeserializeContext ctx)
        {
            return null;
        }
    }
}