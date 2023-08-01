using _2022_csharp.Lib;

namespace _2022_csharp.Day8;

record Command(char Direction, int steps);

static class Day8
{
    public static void Part1()
    {
        var tailVisitedPositions = new HashSet<(int, int)>();
        var commands = Utilities.GetLines("/Day8/TestData.txt")
            .Select(x => x.Split(" "))
            .Select(x => new Command(Convert.ToChar(x[0]), int.Parse(x[1])));

        
        var stop = "";
    }
}