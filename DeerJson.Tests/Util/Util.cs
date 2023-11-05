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