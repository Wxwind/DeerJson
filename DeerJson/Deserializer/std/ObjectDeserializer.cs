using System;
using System.Collections.Generic;

namespace DeerJson.Deserializer.std
{
    public class ObjectDeserializer : JsonDeserializer<object>, IResolvableDeserializer
    {
        private readonly Type                                m_type;
        private readonly Dictionary<string, ISettableMember> m_memberInfoDic;


        public ObjectDeserializer(Type classType, Dictionary<string, ISettableMember> memberInfoDic)
        {
            m_type = classType;
            m_memberInfoDic = memberInfoDic;
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
                    if (!ctx.IsEnabled(JsonFeature.DESERIALIZE_FAIL_ON_UNKNOWN_PROPERTIES))
                    {
                        throw new JsonException($"serializing {m_type.Name}: missing filed {name}'.");
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
    }
}