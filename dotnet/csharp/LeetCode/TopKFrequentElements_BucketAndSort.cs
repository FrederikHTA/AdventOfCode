namespace csharp.LeetCode;

public static class Heap_TopKFrequentElements
{
    public static int[] TopKFrequent_Simple(int[] nums, int k)
    {
        if(nums.Length == 0 || k <= 0)
        {
            return [];
        }
        
        var freq = new Dictionary<int, int>();
        foreach (var n in nums)
        {
            freq[n] = freq.GetValueOrDefault(n) + 1;
        }

        return freq
            .OrderByDescending(p => p.Value)
            .Take(k)
            .Select(p => p.Key)
            .ToArray();
    }
    
    public static int[] TopKFrequent(int[] nums, int k)
    {
        var frequency = new Dictionary<int, int>();
        foreach (var num in nums)
        {
            if (!frequency.ContainsKey(num))
            {
                frequency[num] = 0;
            }
            frequency[num]++;
        }

        // Bucket index = frequency, value = list of numbers with that frequency
        var buckets = new List<int>[nums.Length + 1];
        foreach (var pair in frequency)
        {
            var count = pair.Value;
            buckets[count] ??= [];
            buckets[count].Add(pair.Key);
        }

        var result = new List<int>(k);
        for (var count = buckets.Length - 1; count >= 0 && result.Count < k; count--)
        {
            if (buckets[count] is null) continue;

            foreach (var value in buckets[count])
            {
                result.Add(value);
                if (result.Count == k)
                {
                    break;
                }
            }
        }

        return result.ToArray();
    }
}


