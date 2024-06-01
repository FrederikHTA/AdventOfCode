namespace csharp.Custom;

public class Troels2
{
    // Actual Solution: use a Topologically sorted graph, which finds
    // cycles automatically and orders the tasks during creation of the graph
    /***
     * Input:
     * - [TaskId, Dependencies] list
     *
     * Given:
     * - Execute(id), Find the order in which you can execute tasks without violating dependencies
     *
     * Eksempel:
     * - a, []
     * - b, [a]
     * - c, [a]
     * - d, [a, c]
     *
     * Output:
     * - Execute(a)
     * - Execute(b)
     * - Execute(c)
     * - Execute(d)
     */
    public static void Test()
    {
        /***
         * Assumptions:
         * - Tasks are unique
         * - Dependencies are unique
         * - There can be loops, Throw exception on circular dependencies
         */
        // var tasks = new List<(char, List<char>)>
        // {
        //     ('a', []),
        //     ('b', ['a']),
        //     ('c', ['a']),
        //     ('d', ['a', 'c'])
        // };
        var tasks = new List<(char, List<char>)>
        {
            ('a', []),
            ('c', ['b']),
            ('b', ['a']),
            ('d', ['a', 'c'])
        }.OrderBy(x => x.Item2.Count).ToList();


        var executionOrder = new List<char>();
        var resolvedDependencies = new List<char>();
        var i = 0;
        while (tasks.Count != 0)
        {
            if (i >= tasks.Count)
            {
                throw new Exception("Circular dependencies");
            }

            var task = tasks[i];
            if (tasks[i].Item2.Count == 0)
            {
                executionOrder.Add(task.Item1);
                resolvedDependencies.Add(task.Item1);
                tasks.RemoveAt(i);
                i = 0;
                continue;
            }

            if (tasks[i].Item2.All(x => resolvedDependencies.Contains(x)))
            {
                executionOrder.Add(task.Item1);
                resolvedDependencies.Add(task.Item1);
                tasks.RemoveAt(i);
                i = 0;
                continue;
            }

            i++;
        }

        Console.WriteLine(string.Join("\n", executionOrder));
    }
}
