namespace ConsoleApp1;

public static class PrefixSum_SubarraySumEqualsK
{
    public static int SubarraySum(int[] nums, int k)
    {
        var prefixCount = new Dictionary<int, int>
        {
            [0] = 1
        };

        var sum = 0;
        var result = 0;

        foreach (var num in nums)
        {
            sum += num;
            if (prefixCount.TryGetValue(sum - k, out var count))
            {
                result += count;
            }

            if (!prefixCount.ContainsKey(sum))
            {
                prefixCount[sum] = 0;
            }
            prefixCount[sum]++;
        }

        return result;
    }
}


