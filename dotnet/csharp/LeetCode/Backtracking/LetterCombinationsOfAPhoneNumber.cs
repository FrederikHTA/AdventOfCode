using System.Text;

namespace csharp.LeetCode.Backtracking;

public class LetterCombinationsOfAPhoneNumber
{
    // https://leetcode.com/problems/letter-combinations-of-a-phone-number/
    public static void Run()
    {
        // input can be up to 4 digits
        // Input: digits = "23"
        // Output: ["ad","ae","af","bd","be","bf","cd","ce","cf"]
        // Input: digits = "2"
        // Output: ["a","b","c"]

        var input = "2345";
        var combinations = LetterCombinations(input);
        Console.WriteLine(string.Join(", ", combinations));
    }

    private static readonly Dictionary<char, string> LetterMap = new()
    {
        { '2', "abc" }, { '3', "def" }, { '4', "ghi" }, { '5', "jkl" },
        { '6', "mno" }, { '7', "pqrs" }, { '8', "tuv" }, { '9', "wxyz" },
    };

    private static List<string> LetterCombinations(string digits)
    {
        var result = new List<string>();
        if (digits.Length == 0) return result;

        Backtrack(digits, 0, new StringBuilder(), result);
        return result;
    }

    private static void Backtrack(string digits, int index, StringBuilder sb, List<string> result)
    {
        if (index == digits.Length)
        {
            result.Add(sb.ToString());
            return;
        }

        foreach (var letter in LetterMap[digits[index]])
        {
            sb.Append(letter);
            Backtrack(digits, index + 1, sb, result);
            sb.Length--;
        }
    }

    private static List<string> LetterCombinationsAccumulator(string digits)
    {
        if (digits.Length == 0) return [];

        return digits.Aggregate(
            new List<string> { "" },
            (acc, digit) => [.. acc.SelectMany(prefix => LetterMap[digit].Select(c => prefix + c))]);
    }
}