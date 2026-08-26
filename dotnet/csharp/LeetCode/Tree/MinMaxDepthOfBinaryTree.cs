namespace csharp.LeetCode;

public static class MinMaxDepthOfBinaryTree
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

    // https://leetcode.com/problems/maximum-depth-of-binary-tree/
    public static int MaxDepth(TreeNode root)
    {
        // Input: root = [3,9,20,null,null,15,7]
        // Output: 3
        if (root is null) return 0;
        return 1 + Math.Max(MaxDepth(root.left), MaxDepth(root.right));
    }

    // https://leetcode.com/problems/minimum-depth-of-binary-tree/
    public static int MinDepth(TreeNode root)
    {
        // Input: root = [3,9,20,null,null,15,7]
        // Output: 2
        if (root is null) return 0;
        if (root.left == null) return 1 + MinDepth(root.right);
        if (root.right == null) return 1 + MinDepth(root.left);
        return 1 + Math.Min(MinDepth(root.left), MinDepth(root.right));
    }
}