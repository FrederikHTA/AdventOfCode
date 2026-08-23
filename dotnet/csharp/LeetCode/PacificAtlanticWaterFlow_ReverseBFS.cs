namespace csharp.LeetCode;

public static class ReverseBfs_PacificAtlantic
{
    public static List<List<int>> PacificAtlantic(int[][] heights)
    {
        var rows = heights.Length;
        if (rows == 0) return [];

        var cols = heights[0].Length;
        if (cols == 0) return [];

        var visitedPacific = new bool[rows, cols];
        var visitedAtlantic = new bool[rows, cols];

        var pacificQueue = new Queue<(int r, int c)>();
        var atlanticQueue = new Queue<(int r, int c)>();

        // Pacific borders: top row + left col
        for (var c = 0; c < cols; c++)
        {
            EnqueueIfNew(0, c, visitedPacific, pacificQueue);
        }
        for (var r = 0; r < rows; r++)
        {
            EnqueueIfNew(r, 0, visitedPacific, pacificQueue);
        }

        // Atlantic borders: bottom row + right col
        for (var c = 0; c < cols; c++)
        {
            EnqueueIfNew(rows - 1, c, visitedAtlantic, atlanticQueue);
        }
        for (var r = 0; r < rows; r++)
        {
            EnqueueIfNew(r, cols - 1, visitedAtlantic, atlanticQueue);
        }

        // Reverse flow BFS:
        // from ocean inward, we can move to neighbor if neighbor height >= current height
        Bfs(heights, rows, cols, pacificQueue, visitedPacific);
        Bfs(heights, rows, cols, atlanticQueue, visitedAtlantic);

        var result = new List<List<int>>();
        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                if (visitedPacific[r, c] && visitedAtlantic[r, c])
                {
                    result.Add([r, c]);
                }
            }
        }

        return result;
    }

    private static void Bfs(int[][] heights, int rows, int cols, Queue<(int r, int c)> queue, bool[,] seen)
    {
        while (queue.Count > 0)
        {
            var (r, c) = queue.Dequeue();
            var currentHeight = heights[r][c];

            foreach (var (nr, nc) in GetAdjacent(r, c))
            {
                if (nr < 0 || nr >= rows || nc < 0 || nc >= cols) continue;
                if (seen[nr, nc]) continue;

                // Reverse-flow condition
                if (heights[nr][nc] >= currentHeight)
                {
                    seen[nr, nc] = true;
                    queue.Enqueue((nr, nc));
                }
            }
        }
    }

    private static List<(int r, int c)> GetAdjacent(int r, int c)
    {
        return
        [
            (r - 1, c),
            (r + 1, c),
            (r, c - 1),
            (r, c + 1)
        ];
    }

    private static void EnqueueIfNew(int r, int c, bool[,] seen, Queue<(int r, int c)> queue)
    {
        if (seen[r, c]) return;
        seen[r, c] = true;
        queue.Enqueue((r, c));
    }
}


