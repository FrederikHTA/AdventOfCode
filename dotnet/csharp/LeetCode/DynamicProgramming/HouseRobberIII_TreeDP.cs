namespace csharp.LeetCode;

public static class Dynamic_HouseRobber
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public class Solution
    {
        // https://leetcode.com/problems/house-robber-iii/
        public int Rob(TreeNode root)
        {
            var (skip, rob) = Rec(root);
            return Math.Max(skip, rob);
        }

        private static (int Skip, int Rob) Rec(TreeNode? node)
        {
            if (node is null) return (0, 0);

            var left = Rec(node.left);
            var right = Rec(node.right);

            var skip = Math.Max(left.Rob, left.Skip) + Math.Max(right.Rob, right.Skip);
            var rob = node.val + left.Skip + right.Skip;
            return (skip, rob);
        }
    }
}
