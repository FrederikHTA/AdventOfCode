using System.Diagnostics;

namespace csharp.LeetCode;

public class ReplacementGraph
{
    /***
     * Input:
     * - Streng s
     * - Replacements, i.e. foo -> bar
     *
     * Output:
     * - s med flest mulige replacements
     *
     * Eksempel:
     * - in "ab", {"a" -> "b", "b" -> "c"}
     * - out: "cc"
     */
    public static void Solution()
    {
        var watch = new Stopwatch();
        watch.Start();
        /***
         * Assumptions:
         * - No loops in replacements
         * - No overlapping replacements, i.e. "f" -> "b" and "foo" -> "bar" is not allowed, as f is a subset of foo
         * - There are no sentences, only single words
         * - If 2 words are of equals length, return the first entry
         */
        var input = new[] { "ab", "bc", "foo", "foobar" };
        var replacements = new Dictionary<string, string>
        {
            { "a", "b" },
            { "b", "c" },
            { "foo", "bar" }
        };

        foreach (var replacement in replacements)
        {
            var newValue = replacement.Key;
            while (replacements.TryGetValue(newValue, out var value))
            {
                newValue = value;
            }
            replacements[replacement.Key] = newValue;
        }

        var maxReplacementsString = "";
        foreach (var s in input)
        {
            var replacementCount = 0;
            var tempString = "";
            foreach (var character in s)
            {
                var currentReplacement = character.ToString();
                currentReplacement = Replace(replacements, currentReplacement, ref replacementCount);
                tempString += currentReplacement;
                tempString = Replace(replacements, tempString, ref replacementCount);
            }

            if (replacementCount > maxReplacementsString.Length)
            {
                maxReplacementsString = tempString;
            }
        }

        watch.Stop();
        Console.WriteLine($"String with max replacements: {maxReplacementsString}, runTime: {watch.Elapsed}");
    }

    private static string Replace(Dictionary<string, string> replacements, string temp, ref int replacementCount)
    {
        replacements.TryGetValue(temp, out var value);
        if (value is not null)
        {
            replacementCount++;
            temp = value;
        }

        return temp;
    }
}
