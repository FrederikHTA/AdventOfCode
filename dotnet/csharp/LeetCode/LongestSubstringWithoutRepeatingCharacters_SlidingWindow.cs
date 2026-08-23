namespace csharp.LeetCode;

public static class SlidingWindow_LongestSubstringWithoutRepeat
{
    public static int LengthOfLongestSubstring(string s)
    {
        var lastSeen = new Dictionary<char, int>();
        var leftPos = 0;
        var bestLen = 0;

        for (var rightPos = 0; rightPos < s.Length; rightPos++)
        {
            var ch = s[rightPos];
            if (lastSeen.TryGetValue(ch, out var previous) && previous >= leftPos)
            {
                leftPos = previous + 1;
            }

            lastSeen[ch] = rightPos;
            bestLen = Math.Max(bestLen, rightPos - leftPos + 1);
        }

        return bestLen;
    }
}
