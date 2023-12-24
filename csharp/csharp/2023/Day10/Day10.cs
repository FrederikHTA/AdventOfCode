using csharp.csharp_lib;
using csharp.csharp_lib.Grid;
using csharp.csharp_lib.Pos;
using FluentAssertions;

namespace csharp._2023.Day10;

static class Day10
{
    public static void Part1()
    {
        var input = Utilities.GetLines("/2023/Day10/Data.txt");
        var grid = new Grid<char>(input.Select(x => x.ToCharArray()).ToArray());
        var (x, y) = grid.Data
            .Select((x, i) => (i, x.IndexOf('S')))
            .First(x => x.Item2 != -1);

        var startPos = new Pos(x, y);
        var posSet = new HashSet<Pos> { startPos };

        while (true)
        {
            var currentChar = grid.Get(startPos);
            var north = startPos + new Pos(-1, 0);
            if (!posSet.Contains(north) && grid.ContainsPos(north))
            {
                var northChar = grid.Get(north);
                if (northChar is '|' or '7' or 'F' && currentChar is '|' or 'L' or 'J' or 'S')
                {
                    posSet.Add(north);
                    startPos = north;
                    continue;
                }
            }

            var west = startPos + new Pos(0, -1);
            if (!posSet.Contains(west) && grid.ContainsPos(west))
            {
                var westChar = grid.Get(west);
                if (westChar is '-' or 'L' or 'F' && currentChar is '-' or '7' or 'J' or 'S')
                {
                    posSet.Add(west);
                    startPos = west;
                    continue;
                }
            }

            var east = startPos + new Pos(0, 1);
            if (!posSet.Contains(east) && grid.ContainsPos(east))
            {
                var eastChar = grid.Get(east);
                if (eastChar is '-' or '7' or 'J' && currentChar is '-' or 'L' or 'F' or 'S')
                {
                    posSet.Add(east);
                    startPos = east;
                    continue;
                }
            }

            var south = startPos + new Pos(1, 0);
            if (!posSet.Contains(south) && grid.ContainsPos(south))
            {
                var southChar = grid.Get(south);
                if (southChar is '|' or 'J' or 'L' && currentChar is '|' or '7' or 'F' or 'S')
                {
                    posSet.Add(south);
                    startPos = south;
                    continue;
                }
            }

            break;
        }

        var length = posSet.Count / 2;
        length.Should().Be(6931);
    }

    public static void Part2()
    {
    }
}