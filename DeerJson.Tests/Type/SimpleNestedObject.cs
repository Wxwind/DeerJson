using Newtonsoft.Json;

namespace DeerJson.Tests.Type;

public class SimpleNestedObject : IEquatable<SimpleNestedObject>
{
    public string    str;
    public List<int> numArr;
    public SubObject subObj;

    public SimpleNestedObject()
    {
        subObj = new SubObject();
    }

    public SimpleNestedObject(string str, List<int> numArr, SubObject subObj)
    {
        this.str = str;
        this.numArr = numArr;
        this.subObj = subObj;
    }

    public bool Equals(SimpleNestedObject? other)
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
        return Equals((SimpleNestedObject)obj);
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

public class SubObject : IEquatable<SubObject>
{
    public int  subNum;
    public bool isObj;

    public SubObject()
    {
    }

    public SubObject(int subNum, bool isObj)
    {
        this.subNum = subNum;
        this.isObj = isObj;
    }


    public bool Equals(SubObject? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return subNum == other.subNum && isObj == other.isObj;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SubObject)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(subNum, isObj);
    }

    public override string ToString()
    {
        return $"subNum = {subNum}, isObj = {isObj}";
    }
}