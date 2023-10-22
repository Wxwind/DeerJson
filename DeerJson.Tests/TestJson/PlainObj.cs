namespace DeerJson.Tests.TestJson;

public class PlainObj : IEquatable<PlainObj>
{
    private readonly string name;
    private readonly bool   isAuthor;
    private readonly int    count;

    public PlainObj()
    {
    }

    public PlainObj(string name, bool isAuthor, int count)
    {
        this.name = name;
        this.isAuthor = isAuthor;
        this.count = count;
    }


    public override string ToString()
    {
        return $"name = {name}, isAuthor = {isAuthor},count = {count} ";
    }

    public bool Equals(PlainObj? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return name == other.name && isAuthor == other.isAuthor && count == other.count;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PlainObj)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(name, isAuthor, count);
    }
}