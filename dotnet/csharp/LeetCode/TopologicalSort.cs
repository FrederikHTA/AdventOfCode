using System.Runtime.InteropServices.JavaScript;

namespace csharp.LeetCode;

public class TopologicalSort
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
    public static void Solution()
    {
        /*
         * Assumptions:
         * - Tasks are unique
         * - Dependencies are unique
         * - There can be loops, Throw exception on circular dependencies
         */
        // n = task count, e = total dependency edges
        // Time O(n + e), Space O(n + e) to hold every task and its dependency set
        var graph = new Dictionary<char, HashSet<char>>
        {
            ['a'] = [],
            ['c'] = ['b'],
            ['b'] = ['a'],
            ['d'] = ['a', 'c']
        };

        var adjacency = new Dictionary<char, HashSet<char>>();
        var inDegree = new Dictionary<char, int>();

        foreach (var (course, dependencies) in graph)
        {
            foreach (var dependency in dependencies)
            {
                if (!adjacency.TryGetValue(dependency, out _))
                {
                    adjacency[dependency] = [];
                }
                adjacency[dependency].Add(course);
                inDegree[course] = inDegree.GetValueOrDefault(course) + 1;
            }
        }

        var queue = new Queue<char>();
        var resolved = new List<char>();
        foreach (var node in graph)
        {
            if (node.Value.Count == 0)
            {
                queue.Enqueue(node.Key);
            }
        }

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            resolved.Add(current);

            if (!adjacency.TryGetValue(current, out var adjacents))
            {
                continue;
            }
            
            foreach (var adjacent in adjacents)
            {
                inDegree[adjacent]--;
                if (inDegree[adjacent] == 0)
                {
                    queue.Enqueue(adjacent);
                }
            }
        }

        if (inDegree.Count == 0)
        {
            throw new Exception("Cycle Detected");
        }

        foreach (var c in resolved)
        {
            Console.WriteLine($"Execute({c})");
        }
    }
}