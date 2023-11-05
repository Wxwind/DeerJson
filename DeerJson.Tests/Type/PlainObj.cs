namespace DeerJson.Tests.TestJson;

public class PlainObj : IEquatable<PlainObj>
{
    public  string name;
    private bool   isAuthor;
    private int    count;
    private char   char1;

    public PlainObj()
    {
    }

    public PlainObj(string name, bool isAuthor, int count, char char1)
    {
        this.name = name;
        this.isAuthor = isAuthor;
        this.count = count;
        this.char1 = char1;
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