namespace csharp.LeetCode;

public class TwoSum
{
    // https://leetcode.com/problems/two-sum/
    public static void Run()
    {
        int[] input = [3,2,4];
        var result = TwoSumSolution(input, 6);
        Console.WriteLine(result);
    }

    private static int[] TwoSumSolution(int[] nums, int target)
    {
        var dict = new Dictionary<int, int>();
        for (var i = 0; i < nums.Length; i++)
        {
            var needed = target - nums[i];
            if (dict.TryGetValue(needed, out var otherIndex))
            {
                return [i, otherIndex];
            }
            dict.TryAdd(nums[i], i);
        }

        throw new Exception("Every input shoud have a valid solution");
    }
}