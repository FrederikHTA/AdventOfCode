using csharp.csharp_lib;
using csharp.csharp_lib.Grid;
using FluentAssertions;

namespace csharp._2023.Day11;

static class Day11
{
    public static void Part1()
    {
        const int distance = 2;
        var lines = Utilities.GetLines("/2023/Day11/Data.txt");
        var linesAsCharArray = lines.Select(x => x.ToCharArray().ToList()).ToList();
        var grid = new Grid<char>(linesAsCharArray);

        var galaxyPositions = FindGalaxyPositions(grid);

        AddX(grid, galaxyPositions, distance);

        AddY(grid, galaxyPositions, distance);

        var pairs = GenerateUniquePairs(galaxyPositions);

        pairs.Select(x => Math.Abs(x.Item1.Item1 - x.Item2.Item1) + Math.Abs(x.Item1.Item2 - x.Item2.Item2))
            .Sum()
            .Should()
            .Be(10077850);
    }

    public static void Part2()
    {
        const int distance = 1000000;
        var lines = Utilities.GetLines("/2023/Day11/Data.txt");
        var linesAsCharArray = lines.Select(x => x.ToCharArray().ToList()).ToList();
        var grid = new Grid<char>(linesAsCharArray);

        var galaxyPositions = FindGalaxyPositions(grid);

        AddX(grid, galaxyPositions, distance);
        AddY(grid, galaxyPositions, distance);

        var pairs = GenerateUniquePairs(galaxyPositions);

        pairs
            .Sum(x => Math.Abs(x.Pos1.Item1 - x.Pos2.Item1) + Math.Abs(x.Pos1.Item2 - x.Pos2.Item2))
            .Should()
            .Be(504715068438);
    }

    private static void AddY(Grid<char> grid, List<(long, long)> galaxyPositions, int distance)
    {
        for (var i = grid.Data[0].Count - 1; i > 0; i--)
        {
            if (grid.Data.TrueForAll(x => x[i] == '.'))
            {
                var toIncrease = galaxyPositions.FindAll(x => x.Item2 > i);
                foreach (var pos in toIncrease)
                {
                    var newPos = pos with { Item2 = pos.Item2 + distance - 1 };
                    galaxyPositions.Remove(pos);
                    galaxyPositions.Add(newPos);
                }
            }
        }
    }

    private static void AddX(Grid<char> grid, List<(long, long)> galaxyPositions, int distance)
    {
        for (var i = grid.Data.Count - 1; i > 0; i--)
        {
            if (grid.Data[i].TrueForAll(x => x == '.'))
            {
                var toIncrease = galaxyPositions.FindAll(x => x.Item1 > i);
                foreach (var pos in toIncrease)
                {
                    var newPos = pos with { Item1 = pos.Item1 + distance - 1 };
                    galaxyPositions.Remove(pos);
                    galaxyPositions.Add(newPos);
                }
            }
        }
    }

    private static List<(long, long)> FindGalaxyPositions(Grid<char> universe)
    {
        return universe.Data
            .SelectMany((row, rowIndex) =>
                row.Select((cell, colIndex) => (cell, rowIndex, colIndex)))
            .Where(item => item.cell == '#')
            .Select(item => ((long)item.rowIndex, (long)item.colIndex))
            .ToList();
    }

    private static List<((long, long) Pos1, (long, long) Pos2)> GenerateUniquePairs(List<(long, long)> galaxyPositions)
    {
        return galaxyPositions
            .SelectMany((pos1, i) => galaxyPositions.Skip(i + 1).Select(pos2 => (pos1, pos2)))
            .ToList(); 
    }
}