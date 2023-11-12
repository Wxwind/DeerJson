namespace DeerJson.Tests.Type;

public class PlainObjWithAutoProp : IEquatable<PlainObjWithAutoProp>
{
    private string name { get; set; }
    private bool isAuthor { get; set; }
    private int count { get; set; }
    private char char1;

    public PlainObjWithAutoProp()
    {
    }

    public PlainObjWithAutoProp(string name, bool isAuthor, int count, char char1)
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

    public bool Equals(PlainObjWithAutoProp? other)
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
        return Equals((PlainObjWithAutoProp)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(name, isAuthor, count);
    }
}