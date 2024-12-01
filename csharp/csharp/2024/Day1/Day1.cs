using csharp.csharp_lib;
using FluentAssertions;
namespace csharp._2024.Day1;

static class Day1
{
    public static void Part1()
    {
        var input = Utilities.GetLines("/2024/Day1/Data.txt");
        var data = input.Select(x => x.Split("   ")).Select(x => (int.Parse(x[0]), int.Parse(x[1]))).ToList();
        var left = data.Select(x => x.Item1).Order().ToList();
        var right = data.Select(x => x.Item2).Order().ToList();
        var distances = left.Zip(right).Select(x => Math.Abs(x.First - x.Second)).ToList();
        var result = distances.Sum();
        result.Should().Be(3246517);

        Console.WriteLine(distances.Sum());

    }

    public static void Part2()
    {
        var input = Utilities.GetLines("/2024/Day1/Data.txt");
        var data = input.Select(x => x.Split("   ")).Select(x => (int.Parse(x[0]), int.Parse(x[1]))).ToList();
        var left = data.Select(x => x.Item1).Order().ToList();
        var countDict = data.Select(x => x.Item2).GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        var leftSum = left.Where(x => countDict.ContainsKey(x)).Select(x => x * countDict[x]).ToList();
        var sum = leftSum.Sum();
        sum.Should().Be(29379307);
    }
}
