using ConsoleApp1;

namespace ConsoleApp1;

public class Sorting_MergeIntervals
{
    public static int[][] Merge(int[][] intervals)
    {
        if (intervals.Length == 0) return [];

        var sorted =
            intervals
                .Select(x => (Start: x[0], End: x[1]))
                .OrderBy(x => x.Start)
                .ToArray();

        var merge = new List<(int Start, int End)>(sorted.Length)
        {
            sorted[0]
        };

        for (var i = 1; i < sorted.Length; i++)
        {
            var current = sorted[i];
            var (start, end) = merge[^1];
            if (end >= current.Start)
            {
                merge[^1] = (start, Math.Max(end, current.End));
            }
            else
            {
                merge.Add(current);
            }
        }

        return merge
            .Select(x => new[] { x.Start, x.End })
            .ToArray();
    }
}
