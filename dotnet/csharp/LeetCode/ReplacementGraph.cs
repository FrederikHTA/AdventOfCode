using System.Diagnostics;
namespace csharp.Custom;

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
        foreach (string s in input)
        {
            var replacementCount = 0;
            var tempString = "";
            foreach (char character in s)
            {
                var currentReplacement = character.ToString();
                replacements.TryGetValue(currentReplacement, out string? value);
                if (value is not null)
                {
                    replacementCount++;
                    currentReplacement = value;
                }
                tempString += currentReplacement;

                replacements.TryGetValue(tempString, out string? value2);
                if (value2 is not null)
                {
                    replacementCount++;
                    tempString = value2;
                }
            }

            if (replacementCount > maxReplacementsString.Length)
            {
                maxReplacementsString = tempString;
            }
        }

        watch.Stop();
        Console.WriteLine($"String with max replacements: {maxReplacementsString}, runTime: {watch.Elapsed}");
    }
}
