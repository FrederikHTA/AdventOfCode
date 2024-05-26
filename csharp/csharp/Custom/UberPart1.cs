using FluentAssertions;

namespace csharp.Custom;

public class UberPart1
{
    public static void EasyCountUber()
    {
        List<Tuple<int, int>> coordinates =
        [
            new Tuple<int, int>(4, 7),
            new Tuple<int, int>(-1, 5),
            new Tuple<int, int>(3, 6)
        ];

        var res = coordinates
            .SelectMany(x => Enumerable.Range(x.Item1, x.Item2 - x.Item1 + 1))
            .Distinct()
            .Order()
            .ToList();

        int count = res.Count;
        count.Should().Be(9);
        res.Should().BeEquivalentTo(new List<int>
        {
            -1,
            0,
            1,
            2,
            3,
            4,
            5,
            6,
            7
        });
    }
}
