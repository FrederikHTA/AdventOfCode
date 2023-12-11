using csharp.csharp_lib;

namespace csharp._2023.Day9;

static class Day9
{
    public static void Part1()
    {
        var input = Utilities.GetLines("/2023/Day9/Data.txt");
        var histories = input.Select(line => line.Split().Select(int.Parse).ToList()).ToList();

        var sum = SumOfExtrapolatedValues(histories);

        Console.WriteLine($"Sum of extrapolated values: {sum}");
    }

    public static void Part2()
    {
        var input = Utilities.GetLines("/2023/Day9/TestData2.txt");
        var histories = input.Select(line => line.Split().Select(int.Parse).ToList()).ToList();

        var sum = SumOfExtrapolatedValues(histories);

        Console.WriteLine($"Sum of extrapolated values: {sum}");
    }

    private static int SumOfExtrapolatedValues(List<List<int>> histories)
    {
        var sum = 0;

        foreach (var history in histories)
        {
            var sequences = new List<List<int>> { history };
            var sequence = GetDifferenceSequence(history);
            sequences.Add(sequence);

            while (sequence.Any(diff => diff != 0))
            {
                sequence = GetDifferenceSequence(sequence);
                sequences.Add(sequence);
            }

            foreach (var s in sequences)
            {
                s.Add(int.MaxValue);
            }

            sequence[^1] = 0;

            for (var i = sequences.Count - 1; i > 0; i--)
            {
                var below = sequences[i][^1];
                var left = sequences[i - 1][^2];
                sequences[i - 1][^1] = left + below;
            }

            sum += sequences[0][^1];
        }

        return sum;
    }

    private static List<int> GetDifferenceSequence(List<int> input)
    {
        var result = new List<int>();

        for (var i = 1; i < input.Count; i++)
        {
            result.Add(input[i] - input[i - 1]);
        }

        return result;
    }
}