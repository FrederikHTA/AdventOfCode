using System.Text.RegularExpressions;
using csharp.csharp_lib;

namespace csharp._2023.Day12;

record ConditionRecord(string Springs, List<int> GroupSizes);

static class Day12
{
    public static void Part1()
    {
        var input = Utilities.GetLines("/2023/Day12/TestData.txt");
        var conditionRecords = input.Select(line =>
        {
            var split = line.Split(" ");
            return new ConditionRecord(
                split[0],
                split[1].Split(",").Select(int.Parse).ToList());
        }).ToList();

        var aggregate = new List<int>();
        foreach (var conditionRecord in conditionRecords)
        {
            var springSplits = conditionRecord.Springs
                .Split(['.'])
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();

            var regex = new Regex(@"\#+|\?+");
            var test = regex.Matches(conditionRecord.Springs);

            for (var i = 0; i < springSplits.Count; i++)
            {

            }
        }

        Console.WriteLine(string.Join("\n", aggregate));
        var stop = 0;
    }

    public static void Part2()
    {
    }
}
