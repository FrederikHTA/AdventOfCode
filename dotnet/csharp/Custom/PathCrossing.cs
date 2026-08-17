using System.Diagnostics;

namespace csharp.Custom;

public class PathCrossing
{
    public static void Run()
    {
        var path = "NESWW";
        var result = IsPathCrossing(path);
        Console.WriteLine(result);
    }

    static bool IsPathCrossing(string path)
    {
        if (string.IsNullOrEmpty(path)) throw new ArgumentException("Empty path", nameof(path));

        (int X, int Y) currentPos = (0, 0);
        var visited = new HashSet<(int, int)> { currentPos };

        foreach (var c in path)
        {
            currentPos = c switch
            {
                'N' => (currentPos.X, currentPos.Y + 1),
                'W' => (currentPos.X - 1, currentPos.Y),
                'E' => (currentPos.X + 1, currentPos.Y),
                'S' => (currentPos.X, currentPos.Y - 1),
                _ => throw new ArgumentException("Invalid Direction")
            };

            if (!visited.Add(currentPos))
            {
                return true;
            }
        }

        return false;
    }
}