using System.Text.RegularExpressions;
using csharp.csharp_lib;
using FluentAssertions;

namespace csharp._2023.Day2;

static class Day2
{
    public static void Part1()
    {
        var configuration = new Dictionary<string, int>
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        var games = ParseInput();
        var possibleGames = games
            .Where(x => x.cubes.All(
                y => y.All(z => configuration.ContainsKey(z.Key) && configuration[z.Key] >= z.Value)))
            .Select(x => x.game)
            .Sum();

        possibleGames.Should().Be(2239);
    }

    public static void Part2()
    {
        var games = ParseInput();
        var minimumRequiredCubes = games.Select(x => x.cubes
                .SelectMany(y => y)
                .GroupBy(y => y.Key))
            .Select(x => x.Select(y => y.MaxBy(z => z.Value)))
            .Select(x => x.Select(y => y.Value).Aggregate((x, y) => x * y))
            .Sum();

        minimumRequiredCubes.Should().Be(83435);
    }

    public static List<(int game, IEnumerable<Dictionary<string, int>> cubes)> ParseInput()
    {
        var lines = Utilities.GetLines("/2023/Day2/Data.txt");
        var games = lines.Select(x => x.Split(":"));
        var res = games.Select(x =>
        {
            var game = int.Parse(x[0]
                .Replace(":", "")
                .Split(" ")[1]);

            var regex = new Regex(@"(\d+) (\w+)");

            var cubes = x[1]
                .Split(";")
                .Select(y =>
                {
                    var hashSet = new Dictionary<string, int>();
                    regex.Matches(y)
                        .ForEach(x => hashSet.Add(x.Groups[2].Value, int.Parse(x.Groups[1].Value)));
                    return hashSet;
                });

            return (game, cubes);
        }).ToList();
        return res;
    }
}