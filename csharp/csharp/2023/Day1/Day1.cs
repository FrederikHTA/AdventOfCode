using System.Text.RegularExpressions;
using csharp.csharp_lib;
using FluentAssertions;

namespace csharp._2023.Day1;

static class Day1
{
    public static void Part1()
    {
        var input = Utilities.GetLines("/2023/Day1/Data.txt");
        var regex = new Regex(@"\d");

        var result = input.Select(line => regex.Matches(line))
            .Select(matches => matches.First().Value + matches.Last().Value)
            .Select(int.Parse)
            .Sum();

        result.Should().Be(55029);
    }

    public static void Part2()
    {
        var regex = new Regex(@"\d");
        var input = Utilities.GetLines("/2023/Day1/Data.txt");
        var res = input
            .Select(ReplaceWordsWithLetters);

        var res2 = res.Select(line => regex.Matches(line))
                .Select(matches => matches.First().Value + matches.Last().Value)
                .Select(int.Parse);

        var result = res2.Sum();
        result.Should().Be(55686);
        Console.WriteLine(result);
    }

    private static string ReplaceWordsWithLetters(string input)
    {
        return input
            .Replace("one", "o1e")
            .Replace("two", "t2o")
            .Replace("three", "th3e")
            .Replace("four", "4")
            .Replace("five", "5e")
            .Replace("six", "6")
            .Replace("seven", "7n")
            .Replace("eight", "e8t")
            .Replace("nine", "n9e");
    }
}