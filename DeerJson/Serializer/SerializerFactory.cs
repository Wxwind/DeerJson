using System;
using DeerJson.Serializer.std;
using DeerJson.Serializer.std.Primitive;

namespace DeerJson.Serializer
{
    public class SerializerFactory
    {
        public ISerializer CreateEnumSerializer(Type type)
        {
            return new EnumSerializer();
        }

        public ISerializer CreateObjectSerializer(Type type)
        {
            var builder = new ObjectSerializerBuilder();
            builder.SetType(type);
            var ser = builder.Build();
            return ser;
        }

        public ISerializer FindStdSerializer(Type type)
        {
            if (type == typeof(string))
            {
                return StringSerializer.Instance;
            }

            if (type.IsPrimitive)
            {
                return FindStdPrimitiveSerializer(type);
            }

            throw new JsonException($"not supported std type of '{type}'");
        }

        private ISerializer FindStdPrimitiveSerializer(Type type)
        {
            if (type == typeof(char))
            {
                return CharSerializer.Instance;
            }

            if (type == typeof(bool))
            {
                return BooleanSerializer.Instance;
            }

            if (type == typeof(sbyte))
            {
                return SByteSerializer.Instance;
            }

            if (type == typeof(byte))
            {
                return ByteSerializer.Instance;
            }

            if (type == typeof(short))
            {
                return Int16Serializer.Instance;
            }

            if (type == typeof(ushort))
            {
                return UInt16Serializer.Instance;
            }

            if (type == typeof(int))
            {
                return Int32Serializer.Instance;
            }

            if (type == typeof(uint))
            {
                return UInt32Serializer.Instance;
            }

            if (type == typeof(long))
            {
                return Int64Serializer.Instance;
            }

            if (type == typeof(ulong))
            {
                return UInt64Serializer.Instance;
            }

            if (type == typeof(decimal))
            {
                return DecimalSerializer.Instance;
            }

            if (type == typeof(float))
            {
                return FloatSerializer.Instance;
            }

            if (type == typeof(double))
            {
                return DoubleSerializer.Instance;
            }

            throw new JsonException($"not supported primitive type of '{type}'");
        }
    }
}