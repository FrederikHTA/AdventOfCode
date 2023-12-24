using csharp.csharp_lib;
using csharp.csharp_lib.Grid;
using csharp.csharp_lib.Pos;
using FluentAssertions;

namespace csharp._2023.Day10;

static class Day10
{
    public static void Part1()
    {
        var input = Utilities.GetLines("/2023/Day10/Data.txt");
        var grid = new Grid<char>(input.Select(x => x.ToCharArray()).ToArray());
        var (x, y) = grid.Data
            .Select((x, i) => (i, x.IndexOf('S')))
            .First(x => x.Item2 != -1);

        var startPos = new Pos(x, y);
        var visited = new HashSet<Pos> { startPos };
        var queue = new Queue<Pos>();

        AddStartPositionNodes(startPos, grid, queue);
        FindLoop(queue, visited, grid);

        var length = visited.Count / 2;
        length.Should().Be(6931);
    }

    private static void AddStartPositionNodes(Pos startPos, Grid<char> grid, Queue<Pos> queue)
    {
        var adjacent = startPos.GetAxisOffsets().ToList();
        if (grid.ContainsPos(adjacent[0]) && grid.Get(adjacent[0]) is '|' or '7' or 'F')
            queue.Enqueue(adjacent[0]);
        if (grid.ContainsPos(adjacent[1]) && grid.Get(adjacent[1]) is '-' or 'L' or 'F')
            queue.Enqueue(adjacent[1]);
        if (grid.ContainsPos(adjacent[2]) && grid.Get(adjacent[2]) is '-' or 'J' or '7')
            queue.Enqueue(adjacent[2]);
        if (grid.ContainsPos(adjacent[3]) && grid.Get(adjacent[3]) is '|' or 'L' or 'J')
            queue.Enqueue(adjacent[3]);
    }

    private static void FindLoop(Queue<Pos> queue, HashSet<Pos> visited, Grid<char> grid)
    {
        while (queue.Count != 0)
        {
            var currentPos = queue.Peek();
            if (visited.Contains(currentPos))
            {
                queue.Dequeue();
                continue;
            }

            var currentChar = grid.Get(currentPos);
            var toAdd = currentChar switch
            {
                '|' => (currentPos + new Pos(1, 0), currentPos + new Pos(-1, 0)),
                '-' => (currentPos + new Pos(0, 1), currentPos + new Pos(0, -1)),
                'L' => (currentPos + new Pos(0, 1), currentPos + new Pos(-1, 0)),
                'J' => (currentPos + new Pos(0, -1), currentPos + new Pos(-1, 0)),
                '7' => (currentPos + new Pos(0, -1), currentPos + new Pos(1, 0)),
                'F' => (currentPos + new Pos(0, 1), currentPos + new Pos(1, 0)),
                _ => throw new Exception()
            };

            if (!visited.Contains(toAdd.Item1))
                queue.Enqueue(toAdd.Item1);
            if (!visited.Contains(toAdd.Item2))
                queue.Enqueue(toAdd.Item2);

            visited.Add(currentPos);
            queue.Dequeue();
        }
    }

    public static void Part2()
    {
        // todo
        var input = Utilities.GetLines("/2023/Day10/TestData3.txt");
        var grid = new Grid<char>(input.Select(x => x.ToCharArray()).ToArray());
        var (x, y) = grid.Data
            .Select((x, i) => (i, x.IndexOf('S')))
            .First(x => x.Item2 != -1);

        var startPos = new Pos(x, y);
        var visited = new HashSet<Pos> { startPos };
        var queue = new Queue<Pos>();

        AddStartPositionNodes(startPos, grid, queue);
        FindLoop(queue, visited, grid);
        var minX = visited.Min(x => x.X);
        var minY = visited.Min(x => x.Y);
        var maxX = visited.Max(x => x.X);
        var maxY = visited.Max(x => x.Y);

        for (var xPos = 0; xPos < grid.Height; xPos++)
        {
            for (var yPos = 0; yPos < grid.Width; yPos++)
            {
               // if(xPos < minX || xPos > maxX || yPos < minY || yPos > maxY)
               //     grid.Data[xPos][yPos] = '\'';
               if (visited.Contains(new Pos(xPos, yPos)))
                   grid.Data[xPos][yPos] = 'O';
            }   
        }
        

        var points = 0;
        for (var xPos = 0; xPos < grid.Height; xPos++)
        {
            var isInsideLoop = false;
            for (var yPos = 0; yPos < grid.Width; yPos++)
            {
                if (grid.Data[xPos][yPos] == 'O')
                    isInsideLoop = !isInsideLoop;
                if (grid.Data[xPos][yPos] == '.' && isInsideLoop)
                {
                    points++;
                    grid.Data[xPos][yPos] = 'X';
                }
            }   
        }
        grid.Visualize();
        Console.WriteLine(points);
    }
}