using System;
using System.Collections;
using System.Collections.Generic;
using DeerJson.Deserializer.std;
using DeerJson.Deserializer.std.Key;
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

        private readonly Dictionary<Type, IKeyDeserializer> m_cachedKeyDeserializerDict =
            new Dictionary<Type, IKeyDeserializer>();

        public IKeyDeserializer FindStdKeySerializer(Type type)
        {
            if (m_cachedKeyDeserializerDict.TryGetValue(type, out var deser)) return deser;

            if (type.IsEnum)
            {
                var underlyingType = Enum.GetUnderlyingType(type);
                var d = new EnumKeyDeserializer(type, underlyingType);
                m_cachedKeyDeserializerDict.Add(type, d);
                return d;
            }

            if (type.IsPrimitive || type == typeof(string))
            {
                var d = new StdKeyDeserializer(type);
                m_cachedKeyDeserializerDict.Add(type, d);
                return d;
            }

            throw new JsonException($"not support key of type {type}");
        }

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
            // may just being resolved cuz in dealing with cyclic dependencies.
            if (m_incompleteDeserializers.TryGetValue(type, out var a)) return a;

            // is enum
            if (type.IsEnum)
            {
                var deser = m_factory.CreateEnumDeserializer(type);
                return deser;
            }

            // TODO: support dynamic type collection.
            // is array
            if (type.IsArray)
            {
                return m_factory.CreateArrayDeserializer(type);
            }

            //is list like
            if (typeof(IList).IsAssignableFrom(type))
            {
                return m_factory.CreateListDeserializer(type);
            }

            // is dictionary like
            if (typeof(IDictionary).IsAssignableFrom(type))
            {
                return m_factory.CreateDictionaryDeserializer(type);
            }

            // is string
            if (type == typeof(string))
            {
                return StringDeserializer.Instance;
            }

            // is std primitive type
            if (type.IsPrimitive)
            {
                return m_factory.FindStdPrimitiveDeserializer(type);
            }

            // is class
            if (type.IsClass && !type.IsSubclassOf(typeof(Delegate)) && !(type == typeof(string)))
            {
                // TODO: is generic?
                var deser = type == typeof(JsonNode)
                    ? m_factory.CreateJsonObjectDeserializer()
                    : m_factory.CreateObjectDeserializer(type);
                return deser;
            }

            // is struct
            if (type.IsValueType && !type.IsPrimitive && !type.IsEnum)
            {
                var deser = m_factory.CreateObjectDeserializer(type);
                return deser;
            }


            throw new JsonException($"not supported deserialize of type '{type}'");
        }
    }
}