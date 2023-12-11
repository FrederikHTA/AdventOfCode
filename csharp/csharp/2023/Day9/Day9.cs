using csharp.csharp_lib;
using FluentAssertions;

namespace csharp._2023.Day9;

static class Day9
{
    public static void Part1()
    {
        var input = Utilities.GetLines("/2023/Day9/Data.txt");
        var histories = input.Select(line => line.Split().Select(int.Parse).ToList()).ToList();

        var result = SumOfExtrapolatedValues(histories);

        result.Sum().Should().Be(1584748274);
    }

    public static void Part2()
    {
        var input = Utilities.GetLines("/2023/Day9/Data.txt");
        var histories = input.Select(line => line.Split().Select(int.Parse).ToList()).ToList();

        var result = SumOfExtrapolatedValues2(histories);

        result.Sum().Should().Be(1026);
    }

    private static IEnumerable<int> SumOfExtrapolatedValues2(List<List<int>> histories)
    {
        return histories.Select(history =>
        {
            var sequences = CreateSequences(history, out var sequence);

            sequences.ForEach(x => x.Reverse());
            sequences.ForEach(x => x.Add(int.MaxValue));
            sequence[^1] = 0;

            for (var i = sequences.Count - 1; i > 0; i--)
            {
                var below = sequences[i][^1];
                var left = sequences[i - 1][^2];
                sequences[i - 1][^1] = left - below;
            }

            return sequences[0][^1];
        });
    }


    private static IEnumerable<int> SumOfExtrapolatedValues(List<List<int>> histories)
    {
        return histories.Select(history =>
        {
            var sequences = CreateSequences(history, out var sequence);
            sequences.ForEach(x => x.Add(int.MaxValue));
            sequence[^1] = 0;

            for (var i = sequences.Count - 1; i > 0; i--)
            {
                var below = sequences[i][^1];
                var left = sequences[i - 1][^2];
                sequences[i - 1][^1] = left + below;
            }

            return sequences[0][^1];
        });
    }

    private static List<List<int>> CreateSequences(List<int> history, out List<int> sequence)
    {
        var sequences = new List<List<int>> { history };
        sequence = GetDifferenceSequence(history);
        sequences.Add(sequence);

        while (sequence.Any(diff => diff != 0))
        {
            sequence = GetDifferenceSequence(sequence);
            sequences.Add(sequence);
        }

        return sequences;
    }

    private static List<int> GetDifferenceSequence(IReadOnlyList<int> input)
    {
        return Enumerable.Range(1, input.Count - 1)
            .Select(i => input[i] - input[i - 1])
            .ToList();
    }
}