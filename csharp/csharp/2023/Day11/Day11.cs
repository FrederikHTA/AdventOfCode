using csharp.csharp_lib;
using csharp.csharp_lib.Grid;
using csharp.csharp_lib.Pos;
using FluentAssertions;

namespace csharp._2023.Day11;

static class Day11
{
    public static void Part1()
    {
        var lines = Utilities.GetLines("/2023/Day11/Data.txt");
        var linesAsCharArray = lines.Select(x => x.ToCharArray().ToList()).ToList();
        var grid = new Grid<char>(linesAsCharArray).Transpose();

        var universe = ExpandRows(ExpandRows(grid).Transpose());
        var galaxyPositions = FindGalaxyPositions(universe);
        var pairs = GenerateUniquePairs(galaxyPositions);

        pairs.Select(x => x.Item1.ManhattanDistance(x.Item2))
            .Sum()
            .Should()
            .Be(10077850);
    }

    private static List<Pos> FindGalaxyPositions(Grid<char> universe)
    {
        var galaxyPositions = new List<Pos>();
        for (var row = 0; row < universe.Data.Count; row++)
        {
            for (var col = 0; col < universe.Data[0].Count; col++)
            {
                if (universe.Data[row][col] == '#')
                {
                    galaxyPositions.Add(new Pos(row, col));
                }
            }
        }

        return galaxyPositions;
    }

    private static List<Tuple<Pos, Pos>> GenerateUniquePairs(List<Pos> galaxyPositions)
    {
        var pairs = new List<Tuple<Pos, Pos>>();
        for (var i = 0; i < galaxyPositions.Count - 1; i++)
        {
            for (var j = i + 1; j < galaxyPositions.Count; j++)
            {
                pairs.Add(Tuple.Create(galaxyPositions[i], galaxyPositions[j]));
            }
        }

        return pairs;
    }

    private static Grid<char> ExpandRows(Grid<char> grid)
    {
        var res = new List<List<char>>();
        var newRow = Enumerable.Repeat('.', grid.Data[0].Count).ToList();
        foreach (var row in grid.Data)
        {
            if (row.TrueForAll(x => x == '.'))
            {
                res.Add(newRow);
            }

            res.Add(row);
        }

        return new Grid<char>(res);
    }

    public static void Part2()
    {
    }
}