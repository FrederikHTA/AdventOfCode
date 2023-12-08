using csharp.csharp_lib;
using FluentAssertions;

namespace csharp._2023.Day7;

public record HandAndBid(string Hand, int Bid);

public enum HandType
{
    FiveOfAKind = 7,
    FourOfAKind = 6,
    FullHouse = 5,
    ThreeOfAKind = 4,
    TwoPairs = 3,
    OnePair = 2,
    HighCard = 1
}

static class Day7
{
    public static void Part1()
    {
        var lines = Utilities.GetLines("/2023/Day7/Data.txt");
        var result = GetHandTypes(lines)
            .GroupBy(x => x.handType)
            .Select(x => SortHands(x, false))
            .OrderBy(x => x[0].Item1)
            .SelectMany(x => x)
            .Select((handType, index) => handType.Item2.Bid * (index + 1))
            .Sum();
        
        result.Should().Be(251545216);
    }

    public static void Part2()
    {
        var lines = Utilities.GetLines("/2023/Day7/Data.txt");
        var result = GetHandTypesPart2(lines)
            .GroupBy(x => x.handType)
            .Select(x => SortHands(x, true))
            .OrderBy(x => x[0].Item1)
            .SelectMany(x => x)
            .Select((handType, index) => handType.Item2.Bid * (index + 1))
            .Sum();

        result.Should().Be(250384185);
    }

    private static List<(HandType handType, HandAndBid handAndBid)> GetHandTypes(IEnumerable<string> lines)
    {
        var handTypes = lines.Select(x =>
        {
            var split = x.Split(" ");
            var cards = split[0];
            var bid = int.Parse(split[1]);

            var handType = cards.ToCharArray()
                .GroupBy(x => x)
                .Select(x => new { Card = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            return handType[0].Count switch
            {
                5 => (HandType.FiveOfAKind, new HandAndBid(cards, bid)),
                4 => (HandType.FourOfAKind, new HandAndBid(cards, bid)),
                3 when handType[1].Count == 2 => (HandType.FullHouse, new HandAndBid(cards, bid)),
                3 => (HandType.ThreeOfAKind, new HandAndBid(cards, bid)),
                2 when handType[1].Count == 2 => (HandType.TwoPairs, new HandAndBid(cards, bid)),
                2 => (HandType.OnePair, new HandAndBid(cards, bid)),
                _ => (HandType.HighCard, new HandAndBid(cards, bid))
            };
        }).ToList();

        return handTypes;
    }

    private static List<(HandType handType, HandAndBid handAndBid)> GetHandTypesPart2(IEnumerable<string> lines)
    {
        var handTypes = lines.Select(x =>
        {
            var split = x.Split(" ");
            var cards = split[0];
            var bid = int.Parse(split[1]);

            var jokerCount = cards.Count(x => x == 'J');
            var handType = cards.Replace("J", "")
                .ToCharArray()
                .GroupBy(x => x)
                .Select(x => new { Card = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            if (jokerCount == 5)
            {
                return (HandType.FiveOfAKind, new HandAndBid(cards, bid));
            }

            return (handType[0].Count + jokerCount) switch
            {
                5 => (HandType.FiveOfAKind, new HandAndBid(cards, bid)),
                4 => (HandType.FourOfAKind, new HandAndBid(cards, bid)),
                3 when handType[1].Count == 2 => (HandType.FullHouse, new HandAndBid(cards, bid)),
                3 => (HandType.ThreeOfAKind, new HandAndBid(cards, bid)),
                2 when handType[1].Count == 2 => (HandType.TwoPairs, new HandAndBid(cards, bid)),
                2 => (HandType.OnePair, new HandAndBid(cards, bid)),
                _ => (HandType.HighCard, new HandAndBid(cards, bid))
            };
        }).ToList();

        return handTypes;
    }

    private static List<(HandType, HandAndBid)> SortHands(
        IGrouping<HandType, (HandType, HandAndBid)> grouping,
        bool isPart2)
    {
        return grouping.OrderBy(x => ReplaceCharacters(x.Item2.Hand, isPart2)).ToList();
    }

    private static string ReplaceCharacters(string card, bool isPart2)
    {
        var newCard = card.ToCharArray().Select(x => x switch
        {
            'T' => "10",
            'J' when isPart2 => "00",
            'J' => "11",
            'Q' => "12",
            'K' => "13",
            'A' => "14",
            _ => "0" + x,
        });

        return string.Join("", newCard);
    }
}