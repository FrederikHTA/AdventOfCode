using System.Text;

namespace csharp.LeetCode;
public class MinStack {
    // https://leetcode.com/problems/min-stack/

    private readonly Stack<int> _stack;
    private readonly Stack<int> _minStack;

    public MinStack()
    {
        _stack = new Stack<int>();
        _minStack = new Stack<int>();
    }
    
    public void Push(int value) 
    {
        var currentMin = _minStack.Count == 0 ? value : _minStack.Peek();
        _minStack.Push(Math.Min(value, currentMin));
        _stack.Push(value);
    }

    public void Pop()
    {
        _minStack.Pop();
        _stack.Pop();
    }
    
    public int Top()
    {
        return _stack.Peek();
    }
    
    public int GetMin()
    {
        return _minStack.Peek();
    }
}

public class MinStackRunner()
{
    public static void Run()
    {
        var stack = new MinStack();
    }
}
