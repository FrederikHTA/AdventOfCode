using csharp.csharp_lib;
using FluentAssertions;

namespace csharp._2023.Day6;

public record Input(int Time, int Distance);

static class Day6
{
    public static void Part1()
    {
        var lines = Utilities.GetLines("/2023/Day6/Data.txt").ToList();
        var times = lines[0]["Time:".Length..]
            .Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        var distance = lines[1]["Distance:".Length..]
            .Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        var winsPerGame = new List<int>();
        for (var i = 0; i < times.Count; i++)
        {
            var waysToWin = 0;
            var totalTime = times[i];
            var totalDistance = distance[i];

            for (var holdTime = 0; holdTime < totalTime; holdTime++)
            {
                if (holdTime * (totalTime - holdTime) > totalDistance)
                {
                    waysToWin++;
                }
            }

            winsPerGame.Add(waysToWin);
        }

        var stop = "";
        var result = winsPerGame.Aggregate((a, b) => a * b);
        result.Should().Be(1159152);
    }

    public static void Part2()
    {
        var lines = Utilities.GetLines("/2023/Day6/Data.txt").ToList();
        var times = long.Parse(
            lines[0]["Time:".Length..]
                .Replace(" ", ""));

        var distance = long.Parse(
            lines[1]["Distance:".Length..]
                .Replace(" ", ""));

            var waysToWin = 0;

            for (var holdTime = 0; holdTime < times; holdTime++)
            {
                if (holdTime * (times - holdTime) > distance)
                {
                    waysToWin++;
                }
            }

        var stop = "";
        waysToWin.Should().Be(41513103);
    }
}