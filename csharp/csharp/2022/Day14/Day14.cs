using csharp.csharp_lib.Pos;
using FluentAssertions;

namespace csharp._2022.Day14;

public static class Day14
{
    public static void Part1()
    {
        var positionList = ParseInput("2022/Day14/Data.txt");

        var walls = BuildCaveWalls(positionList);
        var sandRestCount = 0;

        var isFallingIntoTheVoid = false;
        while (!isFallingIntoTheVoid)
        {
            var currentSandPosition = new Pos(500, 0);
            var sandStopped = false;

            while (!sandStopped)
            {
                if (currentSandPosition.Y < walls.Min(x => x.Y))
                {
                    isFallingIntoTheVoid = true;
                    break;
                }

                if (!walls.Contains(currentSandPosition.Down()))
                {
                    currentSandPosition = currentSandPosition.Down();
                    continue;
                }

                if (!walls.Contains(currentSandPosition.DownLeft()))
                {
                    currentSandPosition = currentSandPosition.DownLeft();
                    continue;
                }

                if (!walls.Contains(currentSandPosition.DownRight()))
                {
                    currentSandPosition = currentSandPosition.DownRight();
                    continue;
                }

                sandRestCount++;
                walls.Add(currentSandPosition);
                sandStopped = true;
            }
        }

        sandRestCount.Should().Be(979);
    }

    public static void Part2()
    {
        var positionList = ParseInput("2022/Day14/Data.txt");

        var walls = BuildCaveWalls(positionList);

        var minY = walls.Min(x => x.Y);
        var (minX, maxX) = (walls.Min(x => x.X), walls.Max(x => x.X));

        var min = minX - 200;
        var max = maxX - min + 200;
        var range = Enumerable
            .Range(min, max)
            .Select(x => new Pos(x, minY - 2))
            .ToList();

        walls.UnionWith(range);

        var sandRestCount = 0;
        var isFallingIntoTheVoid = false;
        while (!isFallingIntoTheVoid)
        {
            var currentSandPosition = new Pos(500, 0);
            var sandStopped = false;

            while (!sandStopped)
            {
                if (!walls.Contains(currentSandPosition.Down()))
                {
                    currentSandPosition = currentSandPosition.Down();
                    continue;
                }

                if (!walls.Contains(currentSandPosition.DownLeft()))
                {
                    currentSandPosition = currentSandPosition.DownLeft();
                    continue;
                }

                if (!walls.Contains(currentSandPosition.DownRight()))
                {
                    currentSandPosition = currentSandPosition.DownRight();
                    continue;
                }

                if (currentSandPosition.Equals(new Pos(500, 0)))
                {
                    isFallingIntoTheVoid = true;
                }

                sandRestCount++;
                walls.Add(currentSandPosition);
                sandStopped = true;
            }
        }

        sandRestCount.Should().Be(29044);
    }

    private static void DrawCave(HashSet<Pos> walls, int iteration)
    {
        var xMin = walls.Min(x => x.X);
        var xMax = walls.Max(x => x.X);
        var yMin = walls.Min(x => x.Y);

        Console.WriteLine($"Iteration: {iteration}");
        for (var j = 0; j >= yMin; j--)
        {
            for (var i = xMin - 1; i <= xMax + 1; i++)
            {
                if (walls.TryGetValue(new Pos(i, j), out Pos _))
                {
                    Console.Write("#");
                    continue;
                }

                Console.Write(".");
            }

            Console.WriteLine();
        }

        Console.WriteLine("-------------------------");
    }

    private static HashSet<Pos> BuildCaveWalls(List<List<Pos>> positionList)
    {
        var walls = new HashSet<Pos>();
        foreach (var wallPositions in positionList)
        {
            for (var i = 0; i < wallPositions.Count - 1; i++)
            {
                var start = wallPositions[i];
                var end = wallPositions[i + 1];

                if (start.X == end.X)
                {
                    var min = Math.Min(start.Y, end.Y);
                    var max = Math.Max(start.Y, end.Y);
                    var range = Enumerable.Range(min, max - min + 1);

                    var wallsToAdd = range
                        .Select(y => start with { Y = y })
                        .ToList();

                    walls.UnionWith(wallsToAdd);
                }
                else // iterate on X
                {
                    var min = Math.Min(start.X, end.X);
                    var max = Math.Max(start.X, end.X);
                    var range = Enumerable.Range(min, max - min + 1);

                    var wallsToAdd = range
                        .Select(x => start with { X = x })
                        .ToList();

                    walls.UnionWith(wallsToAdd);
                }
            }
        }

        return walls;
    }

    private static List<List<Pos>> ParseInput(string path)
    {
        return File.ReadLines(path)
            .Select(x => x
                .Split(" -> ")
                .Select(x =>
                {
                    var s = x.Split(",");
                    return new Pos(int.Parse(s[0]), -int.Parse(s[1]));
                }).ToList())
            .ToList();
    }
    
    private static Pos Down(this Pos pos) => new Pos(pos.X, pos.Y - 1);
    private static Pos DownLeft(this Pos pos) => new(pos.X - 1, pos.Y - 1);
    private static Pos DownRight(this Pos pos) => new(pos.X + 1, pos.Y - 1);
}