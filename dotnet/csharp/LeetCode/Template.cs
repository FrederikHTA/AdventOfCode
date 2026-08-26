using System.Text;
using csharp.csharp_lib;

namespace csharp.LeetCode;

static class GenerateParentheses
{
    // https://leetcode.com/problems/generate-parentheses/
    public static void Run()
    {
        // Input: n = 3
        // Output: ["((()))","(()())","(())()","()(())","()()()"]
        var n = 3;
        var result = GenerateParenthesis(n);
        foreach (var res in result)
        {
            Console.Write($"\"{res}\", ");
        }
    }

    private static IList<string> GenerateParenthesis(int n) {
        if (n == 1) return ["()"];

        var result = new List<string>();
        var sb = new StringBuilder();
        Backtrack(n, 0, 0, sb, result);
        return result;
    }

    private static void Backtrack(int n, int open, int closed, StringBuilder sb, List<string> result)
    {
        if (sb.Length == n * 2)
        {
            result.Add(sb.ToString());
            return;
        }

        if (open < n)
        {
            sb.Append('(');
            Backtrack(n, open + 1, closed, sb, result);
            sb.Length--;
        }

        if (closed < open)
        {
            sb.Append(')');
            Backtrack(n, open, closed + 1, sb, result);
            sb.Length--;
        }
    }
}