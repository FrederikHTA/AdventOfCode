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
        // const string s2 = "aabb";
        var result = CheckInclusion(s1, s2);
        Console.WriteLine(result);
    }

    private static bool CheckInclusion(string s1, string s2)
    {
        var need = new int[26];
        var window = new int[26];
        foreach (var c in s1) need[c - 'a']++; 
        
        var matches = 0;

        for (var i = 0; i < s2.Length - 1; i++)
        {
            var current = s2[i] - 'a';
            window[current]++;
            if (window[i] == need[i]) matches++;
            if (window[i] > need[i]) matches--;

            if (i > s1.Length)
            {
                var evict = s2[i - s1.Length] - 'a';
                window[evict]--;
                if (window[evict] == need[evict]) matches++;
            }

            if (matches == s1.Length) return true;
        }

        return false;
    }
}