namespace _2022_csharp.csharp_lib;

public static class Utilities
{
    public static IEnumerable<string> GetLines(string filePath)
    {
        var dir = Directory.GetCurrentDirectory() + filePath;
        return File.ReadLines(dir);
    }
}