namespace csharp.LeetCode.Graph;

public static class Dfs_NumberOfIslands
{
    // https://leetcode.com/problems/number-of-islands/
    public static int NumIslands(char[][] grid)
    {
        if (grid.Length == 0 || grid[0].Length == 0) return 0;

        var rows = grid.Length;
        var cols = grid[0].Length;
        var islands = 0;

        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                if (grid[r][c] != '1') continue;

                islands++;
                FloodWithStack(r, c);
            }
        }

        return islands;

        void FloodWithStack(int startR, int startC)
        {
            var stack = new Stack<(int r, int c)>();
            stack.Push((startR, startC));
            grid[startR][startC] = '0';

            while (stack.Count > 0)
            {
                var (r, c) = stack.Pop();

                foreach (var (nr, nc) in GetAdjacent(r, c))
                {
                    if (!IsWithinBounds(nr, nc)) continue;
                    if (grid[nr][nc] != '1') continue;

                    grid[nr][nc] = '0';
                    stack.Push((nr, nc));
                }
            }
        }

        bool IsWithinBounds(int nr, int nc)
        {
            return nr < 0 || nr >= rows || nc < 0 || nc >= cols;    
        }
        
        static List<(int r, int c)> GetAdjacent(int r, int c)
        {
            return
            [
                (r - 1, c),
                (r + 1, c),
                (r, c - 1),
                (r, c + 1)
            ];
        }
    }
}


