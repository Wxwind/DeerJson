namespace DeerJson
{
    public enum JsonFeature
    {
        SERIALIZE_ORDER_BY_NAME,

        DESERIALIZE_FAIL_ON_UNKNOWN_PROPERTIES,
        DESERIALIZE_FAIL_ON_TRAILING_TOKENS
    }

    public static class JsonFeatureExtension
    {
        public static int GetMask(this JsonFeature f)
        {
            return 1 << (int)f;
        }
    }
}