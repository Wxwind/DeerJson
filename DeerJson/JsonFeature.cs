﻿namespace DeerJson
{
    public enum JsonFeature
    {
        SERIALIZE_ORDER_BY_NAME,
        SERIALIZE_UNDERLYING_TYPE_FOR_ENUM,

        DESERIALIZE_FAIL_ON_UNKNOWN_PROPERTIES,
        DESERIALIZE_FAIL_ON_TRAILING_TOKENS,
        DESERIALIZE_FAIL_ON_NULL_FOR_PRIMITIVES
    }

    public static class JsonFeatureExtension
    {
        public static int GetMask(this JsonFeature f)
        {
            return 1 << (int)f;
        }
    }
}