namespace csharp.LeetCode.ArraysAndSorting;

public class PascalsTriangle
{
    // https://leetcode.com/problems/pascals-triangle/
    public static void Run()
    {
        // Input: numRows = 5
        // Output: [[1],[1,1],[1,2,1],[1,3,3,1],[1,4,6,4,1]]
        const int input = 5;
        var result = Generate(input);
        foreach (var row in result)
        {
            Console.WriteLine(string.Join(" ", row));
        }
    }

    private static IList<IList<int>> Generate(int numRows)
    {
        var result = new List<IList<int>>(numRows);

        for (var row = 0; row < numRows; row++)
        {
            var depth = row + 1;
            result.Add(new List<int>(depth));
            for (var col = 0; col < depth; col++)
            {
                if (col == 0 || col == depth - 1)
                {
                    result[row].Add(1);
                    continue;
                }

                var rowAbove = result[row - 1];
                var left = rowAbove[col - 1];
                var right = rowAbove[col];
                result[row].Add(left + right);
            }
        }

        return result;
    }
}