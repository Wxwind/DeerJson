using System;

namespace DeerJson.Deserializer.std
{
    public class EnumDeserializer : JsonDeserializer<Enum>
    {
        private readonly Type m_type;

        public EnumDeserializer(Type enumType)
        {
            m_type = enumType;
        }

        public override Enum Deserialize(JsonParser p)
        {
            // return Enum.ToObject(value_type, reader.Value);
            throw new NotImplementedException();
        }
    }
}