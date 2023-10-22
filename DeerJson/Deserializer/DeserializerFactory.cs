using System;
using DeerJson.Deserializer.std;
using DeerJson.Deserializer.std.Primitive;

namespace DeerJson.Deserializer
{
    public class DeserializerFactory
    {
        public IDeserializer CreateObjectDeserializer(Type type)
        {
            var builder = new ObjectDeserializerBuilder();
            builder.SetType(type);
            var deser = builder.Build();
            return deser;
        }

        public IDeserializer CreateJsonObjectDeserializer()
        {
            return new JsonNodeDeserializer();
        }

        public IDeserializer CreateEnumDeserializer(Type type)
        {
            var e = Enum.GetUnderlyingType(type);
            return new EnumDeserializer(type);
        }

        public IDeserializer FindStdDeserializer(Type type)
        {
            if (type == typeof(string))
            {
                return StringDeserializer.Instance;
            }

            if (type.IsPrimitive)
            {
                return FindStdScalarDeserializer(type);
            }

            throw new JsonException($"not supported std type of '{type}'");
        }

        private IDeserializer FindStdScalarDeserializer(Type type)
        {
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
                return Int32Deserializer.Instance;
            }

            if (type == typeof(ushort))
            {
                return UInt32Deserializer.Instance;
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
                return Int32Deserializer.Instance;
            }

            if (type == typeof(ulong))
            {
                return UInt32Deserializer.Instance;
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