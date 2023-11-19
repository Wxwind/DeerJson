using System;

namespace DeerJson.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class JsonIgnore : Attribute
    {
    }
}