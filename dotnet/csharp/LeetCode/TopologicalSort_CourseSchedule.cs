namespace csharp.LeetCode;

public class TopologicalSort_CourseSchedule
{
    // var res = CanFinish(4, [[3, 2], [2, 1], [2, 0], [1, 0]]);
    // Console.WriteLine(res);
    // return;

    public static bool CanFinish(int numCourses, int[][] prerequisites)
    {
        var graph = new List<List<int>>();
        for (var i = 0; i < numCourses; i++)
        {
            graph.Add([]);
        }

        var inDegree = new int[numCourses];

        foreach (var p in prerequisites)
        {
            var course = p[0];
            var prerequisite = p[1];
            graph[prerequisite].Add(course);
            inDegree[course]++;
        }

        var queue = new Queue<int>();
        for (var i = 0; i < numCourses; i++)
        {
            if (inDegree[i] == 0)
            {
                queue.Enqueue(i);
            }
        }

        var resolved = 0;
        while (queue.Count > 0)
        {
            var course = queue.Dequeue();
            resolved++;

            foreach (var element in graph[course])
            {
                inDegree[element]--;
                if (inDegree[element] == 0)
                {
                    queue.Enqueue(element);
                }
            }
        }

        return resolved == numCourses;
    }
}
