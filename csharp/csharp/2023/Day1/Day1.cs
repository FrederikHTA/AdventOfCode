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
        var regex = new Regex(@"\d|one|two|three|four|five|six|seven|eight|nine");
        var input = Utilities.GetLines("/2023/Day1/Data.txt");
        var result2 = input.Select(line => regex.Matches(line));
        var res3 = result2.Select(x =>
            {
                var c = GetNumber(x.First().Value) + GetNumber(x.Last().Value);
                return c;
            })
            .Select(int.Parse);

        var result = res3.Sum();

        Console.WriteLine(result);

        var stop = 0;
    }

    private static string GetNumber(string input)
    {
        return input switch
        {
            "one" => "1",
            "two" => "2",
            "three" => "3",
            "four" => "4",
            "five" => "5",
            "six" => "6",
            "seven" => "7",
            "eight" => "8",
            "nine" => "9",
            _ => input
        };
    }
}