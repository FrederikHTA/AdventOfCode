namespace csharp.LeetCode.StringManipulation;

public class MinimumNumberOfFrogsCroaking
{
    // https://leetcode.com/problems/minimum-number-of-frogs-croaking/
    public static void Run()
    {
        var input = "croakcroakcroak";
        var result = MinNumberOfFrogs(input);
        Console.WriteLine(result);
    }

    private static int MinNumberOfFrogs(string croakOfFrogs)
    {
        if (string.IsNullOrEmpty(croakOfFrogs)) throw new ArgumentException("empty input");

        var croakCounts = new int[5]; // c r o a k
        var maxFrogs = 0;

        // Runtime: O(n) - single pass over the input string, constant work per character
        // Space: O(1) - fixed-size 5-element array regardless of input size
        foreach (var i in croakOfFrogs.Select(c => "croak".IndexOf(c)))
        {
            if (i < 0) return -1;

            croakCounts[i]++;
            switch (i)
            {
                // invalid ordering
                case > 0 when croakCounts[i] > croakCounts[i - 1]:
                    return -1;
                // c
                case 0:
                    maxFrogs = Math.Max(maxFrogs, croakCounts[i]);
                    break;
                // k
                case 4:
                {
                    for (var j = 0; j < croakCounts.Length; j++)
                    {
                        croakCounts[j]--;
                    }

                    break;
                }
            }
        }

        if (croakCounts.All(x => x == 0))
        {
            return maxFrogs;
        }

        return -1;
    }
}