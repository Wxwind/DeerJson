using System;
using System.Collections.Generic;
using DeerJson.Deserializer.std;
using DeerJson.Node;

namespace DeerJson.Deserializer
{
    public class DeserializerCache
    {
        private readonly Dictionary<Type, IDeserializer> m_cachedDeserializerDict =
            new Dictionary<Type, IDeserializer>();

        // avoid cyclic dependencies
        private readonly Dictionary<Type, IDeserializer> m_incompleteDeserializers =
            new Dictionary<Type, IDeserializer>();


        private readonly DeserializerFactory m_factory = new DeserializerFactory();

        public IDeserializer FindOrCreateDeserializer(DeserializeContext ctx, Type type)
        {
            if (m_cachedDeserializerDict.TryGetValue(type, out var desc)) return desc;
            var d = CreateAndCacheDeserializer(type);
            m_cachedDeserializerDict.Add(type, d);
            return d;
        }

        private IDeserializer CreateAndCacheDeserializer(Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);
            var realType = underlyingType ?? type;

            var desc = CreateDeserializer(realType);

            if (desc is IResolvableDeserializer d)
            {
                m_incompleteDeserializers.Add(type, desc);
                d.Resolve();
                m_incompleteDeserializers.Remove(type);
            }

            m_cachedDeserializerDict.Add(realType, desc);
            return desc;
        }

        private IDeserializer CreateDeserializer(Type type)
        {
            // may just being resolved cuz you are dealing with cyclic dependencies.
            if (m_incompleteDeserializers.TryGetValue(type, out var a)) return a;

            if (type.IsEnum)
            {
                var desc = m_factory.CreateEnumDeserializer(type);
                return desc;
            }

            if (type.IsClass && !type.IsSubclassOf(typeof(Delegate)))
            {
                // TODO: is generic?
                // is class
                var desc = type == typeof(JsonNode)
                    ? m_factory.CreateJsonObjectDeserializer()
                    : m_factory.CreateObjectDeserializer(type);
                return desc;
            }

            if (type.IsValueType && !type.IsPrimitive && !type.IsEnum)
            {
                // is struct
                var desc = m_factory.CreateObjectDeserializer(type);
                return desc;
            }

            throw new JsonException($"not supported Deserializer of type '{type}'");
        }
    }
}