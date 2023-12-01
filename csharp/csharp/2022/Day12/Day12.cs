using _2022_csharp.csharp_lib;
using _2022_csharp.csharp_lib.Graph;
using _2022_csharp.csharp_lib.Pos;
using FluentAssertions;

namespace _2022_csharp.Day12;

static class Day12
{
    public static void Part1()
    {
        var input = Utilities.GetLines("/Day12/Data.txt").ToArray();

        var mapHeight = input.Length;
        var mapWidth = input[0].Trim().Length;

        var start = FindIndex(input, 'S');
        var end = FindIndex(input, 'E');

        var heightmap = CreateHeightMap(mapHeight, mapWidth, input);

        var result = Dijkstra.GetShortestPath(heightmap, start[0], end[0]);

        result.Should().Be(504);
        Console.WriteLine("Result: " + result);
    }
    
    public static void Part2()
    {
        var input = Utilities.GetLines("/Day12/Data.txt").ToArray();

        var mapHeight = input.Length;
        var mapWidth = input[0].Trim().Length;

        var start = FindIndex(input, 'a');
        var end = FindIndex(input, 'E');

        var heightmap = CreateHeightMap(mapHeight, mapWidth, input);

        var result = start
            .Select(startPosition => Dijkstra.GetShortestPath(heightmap, startPosition, end[0]))
            .Where(x => x > 0)
            .Min();
        
        result.Should().Be(500);
        Console.WriteLine("Result: " + result);
    }

    private static int[,] CreateHeightMap(int mapHeight, int mapWidth, IReadOnlyList<string> input)
    {
        var heightmap = new int[mapHeight, mapWidth];
        for (var x = 0; x < mapHeight; x++)
        {
            for (var y = 0; y < mapWidth; y++)
            {
                heightmap[x, y] = input[x][y] switch
                {
                    'S' => 1,
                    'E' => 26,
                    _ => input[x][y] - 'a' + 1
                };
            }
        }

        return heightmap;
    }

    private static List<Pos> FindIndex(IReadOnlyList<string> input, char charToFind)
    {
        var positions = new List<Pos>();
        for (var row = 0; row < input.Count; row++)
        {
            for (var column = 0; column < input[0].Length; column++)
            {
                if (input[row][column].Equals(charToFind))
                {
                    positions.Add(new Pos(row, column));
                }
            }
        }

        return positions;
    }
}