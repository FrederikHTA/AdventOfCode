namespace csharp.LeetCode;

public static class Backtracking_Subsets
{
    public static IList<IList<int>> Subsets(int[] nums)
    {
        var result = new List<IList<int>>();
        var current = new List<int>();

        void Dfs(int index)
        {
            if (index == nums.Length)
            {
                result.Add(new List<int>(current));
                return;
            }

            // Exclude nums[index]
            Dfs(index + 1);

            // Include nums[index]
            current.Add(nums[index]);
            Dfs(index + 1);
            current.RemoveAt(current.Count - 1);
        }

        Dfs(0);
        return result;
    }
}


