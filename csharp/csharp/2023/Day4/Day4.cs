using System.Text.RegularExpressions;
using csharp.csharp_lib;
using FluentAssertions;

namespace csharp._2023.Day4;

internal record GameCard(int CardNumber, int[] WinningNumbers, int[] MyNumbers)
{
    public static GameCard ParseCard(string cardNumber, string winningNumbers, string myNumbers)
    {
        var winningNumbersArray = winningNumbers.Split(" ")
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(int.Parse)
            .ToArray();

        var myNumbersArray = myNumbers.Split(" ")
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(int.Parse)
            .ToArray();

        return new GameCard(int.Parse(cardNumber), winningNumbersArray, myNumbersArray);
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
        var gameCards = ParseToGameCards("/2023/Day4/Data.txt");

        var result = gameCards
            .Select(x => x.GetScore())
            .Where(x => x > 0)
            .Select(x => Enumerable.Range(1, x).Aggregate((a, _) => a * 2))
            .Sum();

        result.Should().Be(20855);
    }

    public static void Part2()
    {
        var scorePerCard = new Dictionary<int, int>(); 
        var copiesPerCard = new Dictionary<int, int>(); 
        
        var gameCards = ParseToGameCards("/2023/Day4/Data.txt");
        
        gameCards.ForEach(x =>
        {
            var score = x.GetScore();
            scorePerCard.Add(x.CardNumber, score);
            copiesPerCard.Add(x.CardNumber, 1);
        });

        for (var i = 1; i <= gameCards.Count; i++)
        {
            var score = scorePerCard[i];
            var copies = copiesPerCard[i];

            for (var j = i; j < i + score; j++)
            {
                copiesPerCard[j+1] += 1 * copies;
            }
        } 
        
        var result = copiesPerCard
            .Select(x => x.Value)
            .Sum();
        
        result.Should().Be(5489600);
    }

    private static List<GameCard> ParseToGameCards(string path)
    {
        var input = Utilities.GetLines(path);
        var regexLeft = new Regex(@"(?<=Card)\s*(\d+)(?=:)");
        var regexMiddle = new Regex(@"(?<=\:) (\d+|\s+)+(?=\|)");
        var regexRight = new Regex(@"(?<=\|) (\d+|\s+)+");

        var gameCards = input.Select(x => GameCard.ParseCard(
                regexLeft.Match(x).Value,
                regexMiddle.Match(x).Value,
                regexRight.Match(x).Value))
            .ToList();
        return gameCards;
    }
}