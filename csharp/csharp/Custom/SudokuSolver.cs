namespace csharp.Custom;

public static class SudokuSolver
{
    public static void SolveSudoku()
    {
        char[,] sudoku = new[,]
        {
            { '.', '.', '3', '.', '5', '.', '.', '4', '.' },
            { '.', '.', '.', '8', '2', '.', '.', '.', '7' },
            { '.', '.', '.', '.', '.', '3', '.', '6', '9' },
            { '8', '.', '.', '.', '.', '.', '.', '.', '.' },
            { '.', '4', '.', '.', '.', '.', '.', '.', '5' },
            { '7', '.', '.', '.', '.', '1', '.', '9', '.' },
            { '.', '.', '6', '.', '9', '.', '.', '.', '2' },
            { '3', '.', '.', '4', '.', '2', '.', '.', '1' },
            { '.', '1', '.', '.', '.', '.', '.', '.', '.' }
        };

        while (NoOfEmptyCells(sudoku) > 0)
        {
            for (int column = 0; column < 9; column++)
            {
                for (int row = 0; row < 9; row++)
                {
                    if (sudoku[column, row] != '.')
                    {
                        continue;
                    }

                    var possibleValues = GetPossibleValues(sudoku, column, row);
                    if (possibleValues.Count == 1)
                    {
                        sudoku[column, row] = possibleValues[0];
                    }
                }
            }

            VisualizeSudoku(sudoku);
        }

        VisualizeSudoku(sudoku);

        Console.WriteLine("Done!");
    }

    private static void VisualizeSudoku(char[,] sudoku)
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Console.Write(sudoku[i, j] + " ");
            }

            Console.WriteLine();
        }

        Console.WriteLine("--------------------------------------------------");
    }

    private static List<char> GetPossibleValues(char[,] sudoku, int column, int row)
    {
        var possibleValues = new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        for (int index = 0; index < 9; index++)
        {
            if (sudoku[column, index] != '.')
            {
                possibleValues.Remove(sudoku[column, index]);
            }

            if (sudoku[index, row] != '.')
            {
                possibleValues.Remove(sudoku[index, row]);
            }
        }

        int boxStartRow = column - column % 3;
        int boxStartCol = row - row % 3;
        for (int k = boxStartRow; k < boxStartRow + 3; k++)
        {
            for (int l = boxStartCol; l < boxStartCol + 3; l++)
            {
                if (sudoku[k, l] != '.')
                {
                    possibleValues.Remove(sudoku[k, l]);
                }
            }
        }

        return possibleValues;
    }

    private static int NoOfEmptyCells(char[,] sudoku)
    {
        int count = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (sudoku[i, j] == '.')
                {
                    count++;
                }
            }
        }

        return count;
    }
}
