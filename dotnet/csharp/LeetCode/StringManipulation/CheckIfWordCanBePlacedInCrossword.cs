namespace csharp.LeetCode.StringManipulation;

public class CheckIfWordCanBePlacedInCrossword
{
    // https://leetcode.com/problems/check-if-word-can-be-placed-in-crossword/
    public static void Run()
    {
        // Input: board = [["#", " ", "#"], [" ", " ", "#"], ["#", "c", " "]], word = "abc"
        // Output: true
        // Explanation: The word "abc" can be placed as shown above (top to bottom).
        char[][] board = [['#', ' ', '#'], [' ', ' ', '#'], ['#', 'c', ' ']];
        var word = "abc";
        var result = PlaceWordInCrossword(board, word);
        Console.WriteLine(result);
    }

    public static bool PlaceWordInCrossword(char[][] board, string word)
    {
        var rowLength = board.Length;
        var colLength = board[0].Length;
        var reverseWord = new string(word.Reverse().ToArray());

        // Time: O(rowLength * colLength) total across all rows
        // Space: O(1) extra (rows are views into the existing board)
        for (var i = 0; i < rowLength; i++)
        {
            var row = board[i];
            if (ScanLine(row)) return true;
        }

        // Time: O(rowLength * colLength) total across all columns
        // Space: O(rowLength) — one freshly built column array alive at a time
        for (var i = 0; i < colLength; i++)
        {
            var col = Enumerable.Range(0, rowLength).Select(r => board[r][i]).ToArray();
            if (ScanLine(col)) return true;
        }

        return false;

        // Time: O(line.Length), amortized despite the per-match O(word.Length) check
        // Space: O(word.Length) for the transient segment array
        bool ScanLine(char[] line)
        {
            var currentLength = 0;
            for (var i = 0; i <= line.Length; i++)
            {
                // treat running off the end of the line as a boundary, same as '#'
                var currentChar = i < line.Length ? line[i] : '#';
                if (currentChar != '#')
                {
                    currentLength++;
                    continue;
                }

                if (currentLength == word.Length)
                {
                    var segment = line.Skip(i - currentLength).Take(currentLength).ToArray();
                    if (Fits(segment, word) || Fits(segment, reverseWord)) return true;
                }

                currentLength = 0;
            }

            return false;
        }

        // Time: O(segment.Length), Space: O(1)
        bool Fits(char[] segment, string word)
        {
            for (var i = 0; i < segment.Length; i++)
            {
                var current = segment[i];
                if (current == ' ' || current == word[i])
                {
                    continue;
                }

                return false;
            }

            return true;
        }
    }
}