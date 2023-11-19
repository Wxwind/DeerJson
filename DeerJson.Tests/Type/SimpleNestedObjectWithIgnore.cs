using Newtonsoft.Json;

namespace DeerJson.Tests.Type;

public class SimpleNestedObjectWithIgnore : IEquatable<SimpleNestedObjectWithIgnore>
{
    [Attributes.JsonIgnore] private string    str;
    private                         List<int> numArr;
    [Attributes.JsonIgnore] private SubObject subObj;

    public SimpleNestedObjectWithIgnore()
    {
    }

    public SimpleNestedObjectWithIgnore(string str, List<int> numArr, SubObject subObj)
    {
        this.str = str;
        this.numArr = numArr;
        this.subObj = subObj;
    }

    public bool Equals(SimpleNestedObjectWithIgnore? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        if (str == other.str && numArr.SequenceEqual(other.numArr) && subObj == null && other.subObj == null)
        {
            return true;
        }

        return str == other.str && numArr.SequenceEqual(other.numArr) && subObj.Equals(other.subObj);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SimpleNestedObjectWithIgnore)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(str, numArr, subObj);
    }

    public override string ToString()
    {
        return $"str = {str}, numArr = {JsonConvert.SerializeObject(numArr)}, subObj = {{{subObj}}}";
    }
}
