using csharp.csharp_lib;
using FluentAssertions;

namespace csharp._2023.Day5;

public record Map(long Destination, long Source, long Distance);

static class Day5
{
    public static void Part1()
    {
        var input = File.ReadAllText(Directory.GetCurrentDirectory() + "/2023/Day5/Data.txt");
        var lines = input.Split("\r\n\r\n")
            .Select(x => x.Split("\r\n"))
            .ToList();

        var seeds = lines[0][0].Split(":")[1]
            .Split(" ")
            .Where(x => x != "")
            .Select(long.Parse)
            .ToList();

        var maps = lines.Skip(1)
            .Select(group => group.Skip(1)
                .Select(x =>
                {
                    var mapValues = x.Split(" ").Where(y => y != "").Select(long.Parse).ToList();
                    return new Map(mapValues[0], mapValues[1], mapValues[2]);
                }).ToList()).ToList();

        var lowestLocation = new List<long>();
        foreach (var t in seeds)
        {
            var currentValue = t;
            foreach (var currentMap in maps)
            {
                foreach (var row in currentMap)
                {
                    var upperLimit = row.Source + row.Distance;
                    if (row.Source > currentValue || currentValue > upperLimit)
                    {
                        continue;
                    }
                    
                    var diff = row.Destination - row.Source;
                    var res = diff + currentValue;
                    currentValue = res;
                    break;
                }
            }

            lowestLocation.Add(currentValue);
        }

        var result = lowestLocation.Min();
        result.Should().Be(157211394);
    }

    public static void Part2()
    {
    }
}