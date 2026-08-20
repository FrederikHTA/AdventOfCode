using System.Text.RegularExpressions;

namespace csharp.Custom;

public delegate void TreeVisitor<T>(T nodeData);
public class Tree<T>
{
    private T Data;
    private LinkedList<Tree<T>> Children;

    public Tree(T data)
    {
        Data = data;
        Children = new LinkedList<Tree<T>>();
    }

    public void AddChild(T data)
    {
        Children.AddFirst(new Tree<T>(data));
    }

    public Tree<T>? GetChild(int i)
    {
        return Children.FirstOrDefault(n => --i == 0);
    }

    public void Traverse(Tree<T> node, TreeVisitor<T> visitor)
    {
        visitor(node.Data);
        foreach (Tree<T> kid in node.Children)
            Traverse(kid, visitor);
    }
}

public class UberPart2
{
    public static void Solve()
    {
        var input = "(B,C) (C,D) (B,A)";
        var output = "(B(A)(C(D)))";

        var input2 = "(D,E) (F,G) (B,H) (A,B) (B,C) (C,D) (E,F)";
        var output2 = "(A(B(C(D(E(F(G)))))(H))";

        var tree = BuildTree(input);
    }

    private static Regex regex = new Regex(@"\((\w),(\w)\)");
    private static Tree<string> BuildTree(string input)
    {
        string[] split = input.Split(' ');
        string root = split[split.Length / 2];
        string[] left = split.Take(split.Length / 2).ToArray();
        string[] right = split.Skip(split.Length / 2 + 1).ToArray();

        var matches = regex.Matches(root);
        var rootTree = new Tree<string>(matches[0].Groups[1].Value);
        rootTree.AddChild(matches[0].Groups[2].Value);

        foreach (string item in left)
        {
            string[] splitItem = item.Split(',');
            rootTree.AddChild(splitItem[0]);
            rootTree.AddChild(splitItem[1]);
        }

        return rootTree;
    }

    // public static Tree<string> AddChildren(Tree<string> tree)
    // {
    //
    // }
}
