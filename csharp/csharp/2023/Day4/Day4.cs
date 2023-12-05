using System.Text.RegularExpressions;
using csharp.csharp_lib;
using FluentAssertions;

namespace csharp._2023.Day4;

internal record GameCard(int CardNumber, int[] WinningNumbers, int[] MyNumbers)
{
    public static GameCard ParseCard(string gameCard, string winningNumbers, string myNumbers)
    {
        var cardNumber = int.Parse(gameCard);
        var winningNumbersArray = winningNumbers.Split(" ")
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(int.Parse)
            .ToArray();

        var myNumbersArray = myNumbers.Split(" ")
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(int.Parse)
            .ToArray();

        return new GameCard(cardNumber, winningNumbersArray, myNumbersArray);
    }

    public int GetScore()
    {
        return WinningNumbers.Intersect(MyNumbers).Count();
    }
}

static class Day4
{
    public static void Part1()
    {
        var input = Utilities.GetLines("/2023/Day4/Data.txt");
        var regexLeft = new Regex(@"(?<=Card)\s*(\d+)(?=:)");
        var regexMiddle = new Regex(@"(?<=\:) (\d+|\s+)+(?=\|)");
        var regexRight = new Regex(@"(?<=\|) (\d+|\s+)+");

        var gameCards = input.Select(x => GameCard.ParseCard(
                regexLeft.Match(x).Value,
                regexMiddle.Match(x).Value,
                regexRight.Match(x).Value))
            .ToList();

        var result = gameCards
            .Select(x => x.GetScore())
            .Where(x => x > 0)
            .Select(x => Enumerable.Range(1, x).Aggregate((a, _) => a * 2))
            .Sum();

        result.Should().Be(20855);
    }

    public static void Part2()
    {
    }
}