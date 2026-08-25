using System.Text;

namespace csharp.LeetCode;

public class PermutationInString
{
    public static void Run()
    {
        // Input: s1 = "ab", s2 = "eidbaooo"
        // Output: true
        // Explanation: s2 contains one permutation of s1 ("ba").
        const string s1 = "ab";
        const string s2 = "eidbaooo";
        // const string s1 = "aa";
        // const string s2 = "aaa";
        var result = CheckInclusionReadable(s1, s2);
        Console.WriteLine(result);
    }

    private static bool CheckInclusionReadable(string s1, string s2)
    {
        if (s1.Length > s2.Length) return false;

        var need = new Dictionary<char, int>();
        foreach (var c in s1) need[c] = need.GetValueOrDefault(c) + 1;

        var window = new Dictionary<char, int>();

        for (var i = 0; i < s2.Length; i++)
        {
            var addedChar = s2[i];
            window[addedChar] = window.GetValueOrDefault(addedChar) + 1;

            if (i >= s1.Length)
            {
                var evictedChar = s2[i - s1.Length];
                window[evictedChar]--;
                if (window[evictedChar] == 0) window.Remove(evictedChar);
            }

            if (HasSamePermutationCounts(need, window)) return true;
        }

        return false;
    }

    private static bool HasSamePermutationCounts(Dictionary<char, int> need, Dictionary<char, int> window)
    {
        if (need.Count != window.Count) return false;

        foreach (var (c, count) in need)
        {
            if (!window.TryGetValue(c, out var windowCount) || windowCount != count) return false;
        }

        return true;
    }

    private static bool CheckInclusion(string s1, string s2)
    {
        var need = new int[26];
        var window = new int[26];
        foreach (var c in s1) need[c - 'a']++; 
        
        var matches = 0;
        for (int i = 0; i < 26; i++)
        {
            if (need[i] == window[i]) matches++;
        }

        for (var i = 0; i < s2.Length; i++)
        {
            var current = s2[i] - 'a';
            if (window[current] == need[current]) matches--;
            window[current]++;
            if (window[current] == need[current]) matches++;

            if (i >= s1.Length)
            {
                var evict = s2[i - s1.Length] - 'a';
                if (window[evict] == need[evict]) matches--;
                window[evict]--;
                if (window[evict] == need[evict]) matches++;
            }

            if (matches == 26) return true;
        }

        return false;
    }
}