using System;
using System.Collections.Generic;

namespace DeerJson.Serializer.std
{
    public class ObjectSerializer : JsonSerializer<object>, IResolvableSerializer
    {
        private readonly Type                  m_type;
        private readonly List<IWritableMember> m_memberInfoList;

        public ObjectSerializer(Type type, List<IWritableMember> memberInfoList)
        {
            m_type = type;
            m_memberInfoList = memberInfoList;
        }

        public override void Serialize(object value, JsonGenerator gen, SerializeContext ctx)
        {
            gen.WriteObjectStart();

            var memberList = new List<IWritableMember>(m_memberInfoList);
            if (ctx.IsEnabled(JsonFeature.SERIALIZE_ORDER_BY_NAME))
            {
                memberList.Sort((a, b) => a.Name.CompareTo(b.Name));
            }

            foreach (var props in memberList)
            {
                props.SerializeAndWrite(gen, value, ctx);
            }

            gen.WriteObjectEnd();
        }

        public void Resolve(SerializeContext ctx)
        {
            foreach (var sp in m_memberInfoList)
            {
                var desc = ctx.FindSerializer(sp.Type);
                sp.SetSerializer(desc);
            }
        }
    }
}