namespace csharp.LeetCode.Graph;

public class TopologicalSort_CourseScheduleTwo
{
    // https://leetcode.com/problems/course-schedule-ii/
    public static void Run()
    {
        // Input: numCourses = 2, prerequisites = [[1,0]]
        // Output: [0,1]
        // Explanation: There are a total of 2 courses to take.
        // To take course 1 you should have finished course 0. So the correct course order is [0,1].
        
        var result = FindOrder(2, [[1, 0]]);
        Console.WriteLine(string.Join(", ", result));
    }

    public static int[] FindOrder(int numCourses, int[][] prerequisites)
    {
        var adjacency = new List<int>[numCourses];
        var inDegree = new int[numCourses];
        for (var i = 0; i < numCourses; i++) adjacency[i] = [];

        foreach (var prerequisite in prerequisites)
        {
            var course = prerequisite[0];
            var dependsOn = prerequisite[1];
            adjacency[dependsOn].Add(course);
            inDegree[course]++;
        }

        var queue = new Queue<int>();
        for (var i = 0; i < numCourses; i++)
        {
            if (inDegree[i] == 0) queue.Enqueue(i);
        }

        var order = new int[numCourses];
        var resolvedCount = 0;
        while (queue.Count > 0)
        {
            var currentCourse = queue.Dequeue();
            order[resolvedCount] = currentCourse;
            resolvedCount++;
            foreach (var dependent in adjacency[currentCourse])
            {
                inDegree[dependent]--;
                if (inDegree[dependent] == 0)
                {
                    queue.Enqueue(dependent);
                }
            }
        }

        return resolvedCount == numCourses ? order : [];
    }
}