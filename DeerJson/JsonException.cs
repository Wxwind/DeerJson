using System;

namespace DeerJson
{
    public class JsonException : ApplicationException
    {
        public JsonException(string msg) : base(msg)
        {
        }
    }
}