namespace csharp.LeetCode;

public class BinaryTreeRightSideView
{
    public class TreeNode
    {
        public int val;
        public TreeNode? left;
        public TreeNode? right;

        public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
    
    // https://leetcode.com/problems/binary-tree-right-side-view/
    public static void Run()
    {
        // Input: root = [1,2,3,null,5,null,4]
        // Output: [1,3,4]
        // var result = RightSideView(root);
        // Console.WriteLine(result);
    }

    public static List<int> RightSideView(TreeNode root)
    {
        var result = new List<int>();
        Process(root, result, 0);
        return result;
    }

    private static void Process(TreeNode node, List<int> result, int depth)
    {
        if (node is null) return;
        
        if(depth == result.Count) result.Add(node.val);
        Process(node.right, result, depth + 1);
        Process(node.left, result, depth + 1);
    }
}