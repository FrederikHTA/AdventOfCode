using System.Text.RegularExpressions;
using csharp.csharp_lib;

namespace csharp._2023.Day4;

record GameCard(int CardNumber, int[] WinningNumbers, int[] MyNumbers)
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
}

static class Day4
{
    public static void Part1()
    {
        var input = Utilities.GetLines("/2023/Day4/TestData.txt");
        var regexLeft = new Regex(@"(?<=Card) (\d+)(?=:)");
        var regexMiddle = new Regex(@"(?<=\:) (\d+|\s+)+(?=\|)");
        var regexRight = new Regex(@"(?<=\|) (\d+|\s+)+");

        var res = input.Select(x => GameCard.ParseCard(
                regexLeft.Match(x).Value,
                regexMiddle.Match(x).Value,
                regexRight.Match(x).Value))
            .ToList();

        var stop = 0;
        // result.Should().HaveCount(6314);
        // Console.WriteLine("Result: " + result);
    }

    public static void Part2()
    {
    }
}