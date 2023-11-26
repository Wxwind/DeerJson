using DeerJson.Tests.Util;

namespace DeerJson.Tests.Type;

public class NullObj : IEquatable<NullObj>
{
    public List<int> list;
    public string    str;

    public NullObj(List<int> list, string str, char char1, Dictionary<int, string> dic, int num)
    {
        this.list = list;
        this.str = str;
        this.char1 = char1;
        this.dic = dic;
        this.num = num;
    }

    public int num { get; set; }
    public char                    char1;
    public Dictionary<int, string> dic;


    public NullObj()
    {
    }

    public bool Equals(NullObj? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return ((list == null && other.list == null) || list.SequenceEqual(other.list)) && str == other.str &&
               num == other.num && char1 == other.char1 &&
               TypeUtil.Equals(dic, other.dic);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((NullObj)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(list, str, num, char1, dic);
    }
}