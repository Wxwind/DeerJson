using System;
using System.Collections;
using System.Collections.Generic;
using DeerJson.Serializer.std;
using DeerJson.Serializer.std.Key;

namespace DeerJson.Serializer
{
    public class SerializerCache
    {
        private readonly Dictionary<Type, ISerializer> m_cachedSerializerDict =
            new Dictionary<Type, ISerializer>();

        private readonly Dictionary<Type, ISerializer> m_incompleteSerializers =
            new Dictionary<Type, ISerializer>();

        private readonly SerializerFactory m_factory = new SerializerFactory();

        public ISerializer FindStdKeySerializer(Type type)
        {
            if (type.IsEnum)
            {
                return EnumKeySerializer.Instance;
            }

            if (type.IsPrimitive || type == typeof(string))
            {
                return StdKeySerializer.Instance;
            }

            throw new JsonException($"not support key of type {type}");
        }

        public ISerializer FindOrCreateSerializer(SerializeContext ctx, Type type)
        {
            if (m_cachedSerializerDict.TryGetValue(type, out var ser)) return ser;

            // may be in creating deserializer
            if (m_incompleteSerializers.TryGetValue(type, out var ser2)) return ser2;

            var d = CreateAndCacheSerializer(ctx, type);
            return d;
        }

        private ISerializer CreateAndCacheSerializer(SerializeContext ctx, Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);
            var realType = underlyingType ?? type;

            var ser = CreateSerializer(realType);

            if (ser is IResolvableSerializer d)
            {
                m_incompleteSerializers.Add(type, ser);
                d.Resolve(ctx);
                m_incompleteSerializers.Remove(type);
            }

            m_cachedSerializerDict.Add(realType, ser);
            return ser;
        }

        private ISerializer CreateSerializer(Type type)
        {
            // may just being resolved cuz in dealing with cyclic dependencies.
            if (m_incompleteSerializers.TryGetValue(type, out var a)) return a;

            // is enum
            if (type.IsEnum)
            {
                var ser = m_factory.CreateEnumSerializer(type);
                return ser;
            }

            // is array
            if (type.IsArray)
            {
                return m_factory.CreateArraySerializer(type);
            }

            //is list like
            if (typeof(IList).IsAssignableFrom(type))
            {
                return m_factory.CreateListSerializer(type);
            }

            // is dictionary like
            if (typeof(IDictionary).IsAssignableFrom(type))
            {
                return m_factory.CreateDictionarySerializer(type);
            }

            // is string
            if (type == typeof(string))
            {
                return StringSerializer.Instance;
            }

            // is std primitive type
            if (type.IsPrimitive)
            {
                return m_factory.FindStdPrimitiveSerializer(type);
            }

            // is class
            if (type.IsClass && !type.IsSubclassOf(typeof(Delegate)) && !(type == typeof(string)))
            {
                // TODO: is generic?
                var ser = m_factory.CreateObjectSerializer(type);
                return ser;
            }

            // is struct
            if (type.IsValueType && !type.IsPrimitive && !type.IsEnum)
            {
                var ser = m_factory.CreateObjectSerializer(type);
                return ser;
            }

            throw new JsonException($"not supported serialize of type '{type}'");
        }
    }
}