namespace csharp.Custom.Sudoku_Solver;

public static class SudokuSolver
{
    public static void SolveSudoku()
    {
        var sudoku = new[,]
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
            for (var column = 0; column < 9; column++)
            {
                for (var row = 0; row < 9; row++)
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
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
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
        for (var index = 0; index < 9; index++)
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

        var boxStartRow = column - column % 3;
        var boxStartCol = row - row % 3;
        for (var k = boxStartRow; k < boxStartRow + 3; k++)
        {
            for (var l = boxStartCol; l < boxStartCol + 3; l++)
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
        var count = 0;
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
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