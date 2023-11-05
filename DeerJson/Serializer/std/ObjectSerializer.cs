using System;
using System.Collections.Generic;

namespace DeerJson.Serializer.std
{
    public class ObjectSerializer : JsonSerializer<object>, IResolvableSerializer
    {
        private readonly Type                   m_type;
        private readonly List<WritableProperty> m_memberInfoList;

        public ObjectSerializer(Type type, List<WritableProperty> memberInfoList)
        {
            m_type = type;
            m_memberInfoList = memberInfoList;
        }

        public override void Serialize(object value, JsonGenerator gen)
        {
            gen.WriteObjectStart();

            foreach (var props in m_memberInfoList)
            {
                props.SerializeAndWrite(gen, value);
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