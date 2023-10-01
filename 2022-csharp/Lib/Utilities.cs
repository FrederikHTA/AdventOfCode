namespace _2022_csharp.Lib;

public static class Utilities
{
    public static IEnumerable<string> GetLines(string filePath)
    {
        var dir = Directory.GetCurrentDirectory() + filePath;
        return File.ReadLines(dir);
    }
}