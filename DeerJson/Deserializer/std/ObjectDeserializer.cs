using System;
using System.Linq;
using System.Reflection;

namespace DeerJson.Deserializer.std
{
    public class ObjectDeserializer : JsonDeserializer<object>, IResolvableDeserializer
    {
        private readonly Type        m_type;
        private readonly FieldInfo[] m_propsInfo;

        public ObjectDeserializer()
        {
        }

        public ObjectDeserializer(Type classType)
        {
            m_type = classType;
            // Can we deserialze static field and auto props too?
            // now we will get private prop behind auto props here.
            m_propsInfo = m_type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        public override object Deserialize(JsonParser p)
        {
            p.Match(TokenType.LBRACE);
            var o = Activator.CreateInstance(m_type);

            var propname = p.MatchPropName();
            var pi = m_propsInfo.First(a => a.Name == propname);
            while (!p.HasToken(TokenType.RBRACE))
            {
            }

            return o;
        }

        public void Resolve()
        {
            throw new NotImplementedException();
        }
    }
}