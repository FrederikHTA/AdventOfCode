using System.Diagnostics;

namespace csharp.LeetCode;

public class LastDayYouCanStillCross
{
    // https://leetcode.com/problems/last-day-where-you-can-still-cross/
    public static void Run()
    {
        // const int row = 2;
        // const int col = 2;
        // List<List<int>> cellsArr = [[1,1],[2,1],[1,2],[2,2]];
        // List<List<int>> cellsArr = [[1, 1], [1, 2], [2, 1], [2, 2]];

        const int row = 3;
        const int col = 3;
        int[][] cellsArr = [[1, 2], [2, 1], [3, 3], [2, 2], [1, 1], [1, 3], [2, 3], [3, 2], [3, 1]];

        var result = LatestDayToCross(row, col, cellsArr);
        Console.WriteLine(result);
    }

    private static int LatestDayToCross(int row, int col, int[][] cells)
    {
        var zeroIndexedCells = cells.Select(x => (x[0] - 1, x[1] - 1)).ToList();
        if (row <= 0 || col <= 0 || zeroIndexedCells.Count != row * col) return -1;
        
        // dict to quickly lookup whether a cell is flooded or not based on day
        var floodIndex = new Dictionary<(int, int), int>(zeroIndexedCells.Count);
        for (var i = 0; i < zeroIndexedCells.Count; i++) floodIndex[zeroIndexedCells[i]] = i;

        var maxDay = -1;
        var left = 0;
        var right = zeroIndexedCells.Count;
        var day = (int)Math.Floor((double)right / 2);

        while (right - left > 1)
        {
            var hasPath = Bfs(zeroIndexedCells, floodIndex, day, row, col);
            if (hasPath)
            {
                maxDay = Math.Max(day, maxDay);
                left = day;
            }
            else
            {
                right = day;
            }

            day = left + (right - left) / 2;
        }

        return maxDay;
    }

    private static bool Bfs(List<(int, int)> cells, Dictionary<(int, int), int> floodIndex, int day, int rowCount, int colCount)
    {
        var startPositions = GetStartPositions(cells, day);
        var queue = new Queue<(int X, int Y)>(startPositions);
        var visited = new HashSet<(int X, int Y)>();

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (!visited.Add(current)) continue;

            if (IsBottomCell(current.X))
            {
                return true; // path found
            }

            var adjacentCells = GetAdjacentCells(current);
            foreach (var adjacent in adjacentCells)
            {
                if (IsWithinBounds(adjacent) && IsLandCell(adjacent))
                {
                    queue.Enqueue(adjacent);
                }
            }
        }

        return false; // no path

        bool IsLandCell((int X, int Y) cell)
        {
            return floodIndex[cell] >= day;
        }

        bool IsBottomCell(int x)
        {
            return x == rowCount - 1;
        }

        bool IsWithinBounds((int X, int Y) cell)
        {
            return cell.X >= 0 && cell.X <= rowCount - 1 && cell.Y >= 0 && cell.Y <= colCount - 1;
        }

        List<(int X, int Y)> GetAdjacentCells((int X, int Y) cell)
        {
            return
            [
                (cell.X + 1, cell.Y), // L
                (cell.X, cell.Y + 1), // U
                (cell.X, cell.Y - 1), // D
                (cell.X - 1, cell.Y) // R
            ];
        }
    }

    private static List<(int X, int Y)> GetStartPositions(List<(int X, int Y)> cells, int day)
    {
        return cells.Where((t, i) => t.X == 0 && i >= day).Select(cell => (cell.X, cell.Y)).ToList();
    }
}