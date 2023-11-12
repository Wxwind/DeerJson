using System;
using DeerJson.Deserializer.std;
using DeerJson.Deserializer.std.Collection;
using DeerJson.Deserializer.std.Primitive;

namespace DeerJson.Deserializer
{
    public class DeserializerFactory
    {
        public IDeserializer CreateJsonObjectDeserializer()
        {
            return new JsonNodeDeserializer();
        }

        public IDeserializer CreateEnumDeserializer(Type type)
        {
            var e = Enum.GetUnderlyingType(type);
            return new EnumDeserializer(type, e);
        }

        public IDeserializer CreateArrayDeserializer(Type type)
        {
            var e = type.GetElementType();
            return new ArrayDeserializer(type, e);
        }

        public IDeserializer CreateListDeserializer(Type type)
        {
            if (type.IsGenericType)
            {
                var e = type.GetGenericArguments();
                return new ListDeserializer(type, e[0]);
            }

            // TODO: non-generic type
            throw new NotSupportedException($"non-generic type is not supported, will be supported later");
        }

        public IDeserializer CreateDictionaryDeserializer(Type type)
        {
            if (type.IsGenericType)
            {
                var e = type.GetGenericArguments();
                return new DictionaryDeserializer(type, e[0], e[1]);
            }

            // TODO: non-generic type
            throw new NotSupportedException($"non-generic type is not supported, will be supported later");
        }

        public IDeserializer CreateObjectDeserializer(Type type)
        {
            var builder = new ObjectDeserializerBuilder();
            builder.SetType(type);
            var deser = builder.Build();
            return deser;
        }

        public IDeserializer FindStdPrimitiveDeserializer(Type type)
        {
            if (type == typeof(char))
            {
                return CharDeserializer.Instance;
            }
            
            if (type == typeof(bool))
            {
                return BooleanDeserializer.Instance;
            }

            if (type == typeof(sbyte))
            {
                return SByteDeserializer.Instance;
            }

            if (type == typeof(byte))
            {
                return ByteDeserializer.Instance;
            }

            if (type == typeof(short))
            {
                return Int16Deserializer.Instance;
            }

            if (type == typeof(ushort))
            {
                return UInt16Deserializer.Instance;
            }

            if (type == typeof(int))
            {
                return Int32Deserializer.Instance;
            }

            if (type == typeof(uint))
            {
                return UInt32Deserializer.Instance;
            }

            if (type == typeof(long))
            {
                return Int64Deserializer.Instance;
            }

            if (type == typeof(ulong))
            {
                return UInt64Deserializer.Instance;
            }

            if (type == typeof(decimal))
            {
                return DecimalDeserializer.Instance;
            }

            if (type == typeof(float))
            {
                return FloatDeserializer.Instance;
            }

            if (type == typeof(double))
            {
                return DoubleDeserializer.Instance;
            }

            throw new JsonException($"not supported primitive type of '{type}'");
        }
    }
}