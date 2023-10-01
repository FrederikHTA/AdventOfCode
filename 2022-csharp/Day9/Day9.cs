using _2022_csharp.Lib;
using _2022_csharp.Lib.Pos;
using FluentAssertions;

namespace _2022_csharp.Day9;

enum Direction
{
    U,
    D,
    L,
    R
}

record Command(Direction Direction, int steps);

static class Day9
{
    public static void Part1()
    {
        var tPosVisited = CalculateVisited(numberOfKnots: 2);

        tPosVisited.Should().HaveCount(6314);
        Console.WriteLine("VisitedCount: " + tPosVisited.Count);
    }
    
    public static void Part2()
    {
        var tPosVisited = CalculateVisited(numberOfKnots: 10);

        tPosVisited.Should().HaveCount(2504);
        Console.WriteLine("VisitedCount: " + tPosVisited.Count);
    }

    private static HashSet<Pos> CalculateVisited(int numberOfKnots)
    {
        var commands = ParseToCommands("/Day8/Data.txt");
        var tPosVisited = new HashSet<Pos> { new(0, 0) };

        var knots = Enumerable.Range(0, numberOfKnots).Select(x => new Pos(0,0)).ToList();

        commands.ForEach(command =>
        {
            Enumerable.Range(0, command.steps).ForEach(_ =>
            {
                knots[0] = command.Direction switch
                {
                    Direction.U => knots[0] + new Pos(0, 1),
                    Direction.D => knots[0] + new Pos(0, -1),
                    Direction.L => knots[0] + new Pos(-1, 0),
                    Direction.R => knots[0] + new Pos(1, 0),
                    _ => throw new ArgumentOutOfRangeException("i died")
                };

                for (var i = 1; i < knots.Count; i++)
                {
                    var isTouching = knots[i] == knots[i - 1] || knots[i].GetAllOffsets().Contains(knots[i - 1]);

                    if (isTouching) return;

                    var xDist = knots[i - 1].X - knots[i].X;
                    var yDist = knots[i - 1].Y - knots[i].Y;

                    knots[i] += new Pos(Math.Sign(xDist), Math.Sign(yDist));
                }

                tPosVisited.Add(knots.Last());
            });
        });
        
        return tPosVisited;
    }

    private static IEnumerable<Command> ParseToCommands(string filePath)
    {
        return Utilities.GetLines(filePath)
            .Select(x => x.Split(" "))
            .Select(x => new Command(
                Direction: Enum.Parse<Direction>(x[0], true),
                steps: int.Parse(x[1])));
    }
}