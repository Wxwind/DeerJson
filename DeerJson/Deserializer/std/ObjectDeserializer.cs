using System;
using System.Collections.Generic;

namespace DeerJson.Deserializer.std
{
    public class ObjectDeserializer : JsonDeserializer<object>, IResolvableDeserializer
    {
        private readonly Type                                 m_type;
        private readonly Dictionary<string, SettableProperty> m_propertyInfoDic;


        public ObjectDeserializer(Type classType, Dictionary<string, SettableProperty> propertyInfoDic)
        {
            m_type = classType;
            m_propertyInfoDic = propertyInfoDic;
        }

        public override object Deserialize(JsonParser p)
        {
            p.Match(TokenType.LBRACE);
            var o = Activator.CreateInstance(m_type);
            
            while (!p.HasToken(TokenType.RBRACE))
            {
                var propName = p.GetString();
                p.Match(TokenType.COLON);
                if (m_propertyInfoDic.TryGetValue(propName, out var settableProperty))
                {
                    settableProperty.DeserializeAndSet(p, o);
                }
                else
                {
                    // TODO: Support skipping redundancy json fields by config
                    throw new JsonException($"serializing {m_type.Name}: missing filed {propName}'.");
                }

                // skip comma after obj pair
                if (p.HasToken(TokenType.COMMA))
                {
                    p.Match(TokenType.COMMA);
                    if (p.HasToken(TokenType.RBRACE))
                    {
                        throw new JsonException("trailing comma is not allowed");
                    }
                }
            }

            return o;
        }

        public void Resolve(DeserializeContext ctx)
        {
            foreach (var sp in m_propertyInfoDic.Values)
            {
                var desc = ctx.FindDeserializer(sp.Type);
                sp.SetDeserializer(desc);
            }
        }
    }
}