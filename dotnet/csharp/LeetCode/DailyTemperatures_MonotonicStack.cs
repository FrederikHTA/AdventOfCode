namespace csharp.LeetCode;

public static class MonotonicStack_DailyTemperatures
{
    public static int[] DailyTemperatures(int[] temperatures)
    {
        var result = new int[temperatures.Length];
        var stack = new Stack<int>(); // decreasing stack of indices by temperature

        for (var i = 0; i < temperatures.Length; i++)
        {
            while (stack.Count > 0 && temperatures[i] > temperatures[stack.Peek()])
            {
                var previousIndex = stack.Pop();
                result[previousIndex] = i - previousIndex;
            }

            stack.Push(i);
        }

        return result;
    }
}


