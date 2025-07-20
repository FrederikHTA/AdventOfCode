using csharp.csharp_lib;
using csharp.csharp_lib.Grid;
using csharp.csharp_lib.Pos;
using FluentAssertions;

namespace csharp._2023.Day3;

static class Day3
{
    public static void Part1()
    {
        var input = Utilities.GetLines("/2023/Day3/Data.txt").ToList();
        var grid = new Grid<char>(input.Select(x => x.Select(y => y).ToList()).ToList());

        var visitedPositions = new HashSet<Pos>();
        var partNumbers = new List<int>();
        for (var row = 0; row < grid.Height; row++)
        {
            for (var col = 0; col < grid.Width; col++)
            {
                var pos = new Pos(row, col);
                if (grid.Get(pos) == '.' || char.IsDigit(grid.Get(pos)))
                {
                    continue;
                }

                var offsets = pos.GetAllOffsets()
                    .Where(x => char.IsDigit(grid.Get(x)) && !visitedPositions.Contains(x));

                foreach (var offset in offsets)
                {
                    Calculate(visitedPositions, grid, offset, partNumbers);
                }
            }
        }

        partNumbers.Sum().Should().Be(522726);
    }

    public static void Part2()
    {
        var input = Utilities.GetLines("/2023/Day3/Data.txt").ToList();
        var grid = new Grid<char>(input.Select(x => x.Select(y => y).ToList()).ToList());

        var visitedPositions = new HashSet<Pos>();
        var gearRatios = new List<int>();
        for (var row = 0; row < grid.Height; row++)
        {
            for (var col = 0; col < grid.Width; col++)
            {
                var pos = new Pos(row, col);
                if (grid.Get(pos) != '*')
                {
                    continue;
                }

                var offsets = pos.GetAllOffsets()
                    .Where(x => char.IsDigit(grid.Get(x)) && !visitedPositions.Contains(x));

                var gears = new List<int>();
                foreach (var offset in offsets)
                {
                    Calculate(visitedPositions, grid, offset, gears);
                }

                if (gears.Count != 2)
                {
                    continue;
                }

                gearRatios.Add(gears.Aggregate((a, b) => a * b));
            }
        }

        gearRatios.Sum().Should().Be(81721933);
    }

    private static void Calculate(HashSet<Pos> visitedPositions, Grid<char> grid, Pos offset, List<int> gears)
    {
        var numberPositions = new HashSet<Pos>();
        if (visitedPositions.Contains(new Pos(offset.X, offset.Y)))
        {
            return;
        }

        numberPositions.Add(offset);

        var left = offset + new Pos(0, -1);
        while (grid.ContainsPos(left) && char.IsDigit(grid.Get(left)))
        {
            visitedPositions.Add(left);
            numberPositions.Add(left);
            left += new Pos(0, -1);
        }

        var right = offset + new Pos(0, 1);
        while (grid.ContainsPos(right) && char.IsDigit(grid.Get(right)))
        {
            visitedPositions.Add(right);
            numberPositions.Add(right);
            right += new Pos(0, 1);
        }

        var join = string.Join("", numberPositions.OrderBy(x => x.Y)
            .Select(x => grid.Get(x))
            .ToList());

        gears.Add(int.Parse(join));
        visitedPositions.Add(offset);
    }
}