namespace csharp.LeetCode;

public class LongestPalindromicSubstring
{
    // https://leetcode.com/problems/longest-palindromic-substring/
    public string LongestPalindrome(string s)
    {
        if (string.IsNullOrEmpty(s) || s.Length == 1) return s;

        var bestLen = 1;
        var bestStart = 0;

        for (var i = 0; i < s.Length; i++)
        {
            Expand(i, i);
            Expand(i, i + 1);
        }

        var substring = s.Substring(bestStart, bestLen);
        return substring;

        void Expand(int left, int right)
        {
            while (left >= 0 && right < s.Length && s[left] == s[right])
            {
                var len = right - left + 1;
                if (len > bestLen)
                {
                    bestLen = len;
                    bestStart = left;
                }

                left--;
                right++;
            }
        }
    }
}
