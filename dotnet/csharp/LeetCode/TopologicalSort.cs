namespace csharp.Custom;

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

        // adjacency[dependency] = tasks that depend on it (outgoing edges)
        // Time O(n + e): visits every task once and every edge once
        // Space O(n + e): worst case one adjacency entry per node, one dependent per edge
        var adjacency = new Dictionary<char, HashSet<char>>();
        foreach (var (node, dependencies) in graph)
        {
            foreach (var dependency in dependencies)
            {
                if (adjacency.TryGetValue(dependency, out var dependents))
                {
                    dependents.Add(node);
                    continue;
                }

                adjacency.Add(dependency, [node]);
            }
        }

        // Time O(n) to scan tasks for the initial no-dependency set
        // Space O(n) worst case (every task starts with zero dependencies)
        var resultOrder = new Queue<char>();
        var taskQueue = new Queue<char>();
        foreach (var (node, dependencies) in graph)
        {
            if (dependencies.Count == 0)
            {
                taskQueue.Enqueue(node);
            }
        }

        // Kahn's core: each task is dequeued once (O(n)) and each edge is
        // traversed/removed once (O(e)), HashSet.Remove being O(1) average
        // Time O(n + e), Space O(n) for the queue and result (already counted above)
        while (taskQueue.Count > 0)
        {
            var current = taskQueue.Dequeue();
            resultOrder.Enqueue(current);

            if (!adjacency.TryGetValue(current, out var dependents))
            {
                continue;
            }

            foreach (var dependent in dependents)
            {
                var deps = graph[dependent];
                deps.Remove(current);
                if (deps.Count == 0)
                {
                    taskQueue.Enqueue(dependent);
                }
            }
        }

        // Time O(n) to check every task's remaining dependency set, Space O(1)
        if (graph.Any(x => x.Value.Count > 0))
        {
            throw new Exception("Cycle detected in graph");
        }

        // Time O(n) to print each resolved task, Space O(1) extra
        foreach (var c in resultOrder)
        {
            Console.WriteLine($"Execute({c})");
        }
    }
}