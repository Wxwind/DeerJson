namespace DeerJson.Tests.Util;

public static class ReadUtil
{
    public static string LoadJSON(string fileName)
    {
        var filePath = Path.Combine("../../../TestJson", fileName);
        var json = File.ReadAllText(filePath);
        return json;
    }
}

public static class TypeUtil
{
    public static bool Equals<T1, T2>(Dictionary<T1, T2> d1, Dictionary<T1, T2> d2)
    {
        if (d1 == null)
        {
            return d2 == null ? true : false;
        }

        if (ReferenceEquals(d1, d2)) return true;
        if (d1.Count != d2.Count)
        {
            return false;
        }

        return d1.All(t1 => d2.FirstOrDefault(t2 => t2.Key.Equals(t1.Key)).Value.Equals(t1.Value));
    }
}