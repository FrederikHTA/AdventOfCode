namespace csharp.LeetCode.Backtracking;

public class Permutations
{
    // https://leetcode.com/problems/permutations/
    public static void Run()
    {
        // Input: nums = [1,2,3]
        // Output: [[1,2,3],[1,3,2],[2,1,3],[2,3,1],[3,1,2],[3,2,1]]

        int[] input = [1,2,3];
        var combinations = Permute(input);
        foreach (var combination in combinations)
        {
            foreach (var i in combination)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();
        }
    }

    private static IList<IList<int>> Permute(int[] nums)
    {
        var result = new List<IList<int>>();
        var path = new List<int>();
        var used = new HashSet<int>();
        Backtrack(nums, path, used, result);
        return result;
    }
    
    private static void Backtrack(int[] nums, List<int> path, HashSet<int> used, List<IList<int>> result)
    {
        if(path.Count == nums.Length)
        {
            result.Add(new List<int>(path));
            return;
        }

        foreach (var current in nums)
        {
            if(!used.Add(current)) continue;
            
            path.Add(current);
            Backtrack(nums, path, used, result);
            used.Remove(current);
            path.RemoveAt(path.Count - 1);
        }
    }

    private static IList<IList<int>> PermuteSwap(int[] nums)
    {
        var result = new List<IList<int>>();
        BacktrackSwap(nums, 0, result);
        return result;
    }

    private static void BacktrackSwap(int[] nums, int start, List<IList<int>> result)
    {
        if (start == nums.Length)
        {
            result.Add(new List<int>(nums));
            return;
        }

        for (var i = start; i < nums.Length; i++)
        {
            (nums[start], nums[i]) = (nums[i], nums[start]);
            BacktrackSwap(nums, start + 1, result);
            (nums[start], nums[i]) = (nums[i], nums[start]); // swap back
        }
    }
}