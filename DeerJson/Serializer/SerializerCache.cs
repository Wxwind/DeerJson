using System;
using System.Collections.Generic;

namespace DeerJson.Serializer
{
    public class SerializerCache
    {
        private readonly Dictionary<Type, ISerializer> m_cachedSerializerDict =
            new Dictionary<Type, ISerializer>();

        private readonly Dictionary<Type, ISerializer> m_incompleteSerializers =
            new Dictionary<Type, ISerializer>();

        private readonly SerializerFactory m_factory = new SerializerFactory();

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
            if (type.IsEnum)
            {
                var ser = m_factory.CreateEnumSerializer(type);
                return ser;
            }

            if (type.IsClass && !type.IsSubclassOf(typeof(Delegate)) && !(type == typeof(string)))
            {
                // TODO: is generic?
                // is class
                var ser = m_factory.CreateObjectSerializer(type);
                return ser;
            }

            if (type.IsValueType && !type.IsPrimitive && !type.IsEnum)
            {
                // is struct
                var ser = m_factory.CreateObjectSerializer(type);
                return ser;
            }

            var d = m_factory.FindStdSerializer(type);
            if (d != null)
            {
                return d;
            }

            throw new JsonException($"not supported serializer of type '{type}'");
        }
    }
}