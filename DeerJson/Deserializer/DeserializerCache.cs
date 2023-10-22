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
            if (m_cachedDeserializerDict.TryGetValue(type, out var deser)) return deser;

            // may be in creating deserializer
            if (m_incompleteDeserializers.TryGetValue(type, out var deser2)) return deser2;

            var d = CreateAndCacheDeserializer(ctx, type);
            return d;
        }

        private IDeserializer CreateAndCacheDeserializer(DeserializeContext ctx, Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);
            var realType = underlyingType ?? type;

            var deser = CreateDeserializer(realType);

            if (deser is IResolvableDeserializer d)
            {
                m_incompleteDeserializers.Add(type, deser);
                d.Resolve(ctx);
                m_incompleteDeserializers.Remove(type);
            }

            m_cachedDeserializerDict.Add(realType, deser);
            return deser;
        }

        private IDeserializer CreateDeserializer(Type type)
        {
            // may just being resolved cuz you are dealing with cyclic dependencies.
            if (m_incompleteDeserializers.TryGetValue(type, out var a)) return a;

            if (type.IsEnum)
            {
                var deser = m_factory.CreateEnumDeserializer(type);
                return deser;
            }

            if (type.IsClass && !type.IsSubclassOf(typeof(Delegate)) && !(type == typeof(string)))
            {
                // TODO: is generic?
                // is class
                var deser = type == typeof(JsonNode)
                    ? m_factory.CreateJsonObjectDeserializer()
                    : m_factory.CreateObjectDeserializer(type);
                return deser;
            }

            if (type.IsValueType && !type.IsPrimitive && !type.IsEnum)
            {
                // is struct
                var deser = m_factory.CreateObjectDeserializer(type);
                return deser;
            }

            var d = m_factory.FindStdDeserializer(type);
            if (d != null)
            {
                return d;
            }
            throw new JsonException($"not supported Deserializer of type '{type}'");
        }
    }
}