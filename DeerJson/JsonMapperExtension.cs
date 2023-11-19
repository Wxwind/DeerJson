namespace DeerJson
{
    public static class JsonMapperExtension
    {
        public static T ParseJson<T>(this string value)
        {
            return JsonMapper.Default.ParseJson<T>(value);
        }

        public static string ToJson(this object value)
        {
            return JsonMapper.Default.ToJson(value);
        }
    }
}