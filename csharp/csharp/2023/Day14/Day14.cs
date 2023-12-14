using csharp.csharp_lib;
using csharp.csharp_lib.Grid;
using csharp.csharp_lib.Pos;
using FluentAssertions;

namespace csharp._2023.Day14;

static class Day14
{
    public static void Part1()
    {
        var input = Utilities.GetLines("/2023/Day14/Data.txt")
            .Select(x => x.ToCharArray().ToList())
            .ToList();

        var grid = new Grid<char>(input);
        MoveRocks(grid, new Pos(-1, 0));

        grid.Data
            .Select((x, i) => x.Count(y => y == 'O') * (grid.Height - i))
            .Sum()
            .Should()
            .Be(105461);
    }

    public static void Part2()
    {
        var input = Utilities.GetLines("/2023/Day14/Data.txt")
            .Select(x => x.ToCharArray().ToList())
            .ToList();

        var grid = new Grid<char>(input);

        for (var i = 0; i < 1000; i++)
        {
            MoveRocks(grid, new Pos(-1, 0));
            grid = grid.Rotate();
            MoveRocks(grid, new Pos(-1, 0));
            grid = grid.Rotate();
            MoveRocks(grid, new Pos(-1, 0));
            grid = grid.Rotate();
            MoveRocks(grid, new Pos(-1, 0));
            grid = grid.Rotate();
        }

        grid.Data
            .Select((x, i) => x.Count(y => y == 'O') * (grid.Height - i))
            .Sum()
            .Should()
            .Be(102829);
    }

    private static void MoveRocks(Grid<char> grid, Pos direction)
    {
        for (var i = 0; i < grid.Height; i++)
        {
            for (var j = 0; j < grid.Width; j++)
            {
                var pos = new Pos(i, j);
                var above = pos + direction;

                if (grid.Get(pos) != 'O')
                {
                    continue;
                }

                while (grid.ContainsPos(above) && grid.Get(above) == '.')
                {
                    grid.Data[pos.X][pos.Y] = '.';
                    above += direction;
                    pos += direction;
                    grid.Data[pos.X][pos.Y] = 'O';
                }
            }
        }
    }
}