using DeerJson.Deserializer.std;

namespace DeerJson.Deserializer
{
    public class ObjectDeserializerFactory
    {
        public void SetType()
        {
        }

        public ObjectDeserializer Build()
        {
            return new ObjectDeserializer();
        }
    }
}