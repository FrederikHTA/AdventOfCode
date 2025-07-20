namespace csharp.csharp_lib.Pos;

public record Pos(int X, int Y)
{
    public static Pos operator +(Pos p1, Pos p2) =>
        new(p1.X + p2.X, p1.Y + p2.Y);
    
    public static Pos operator -(Pos p1, Pos p2) =>
        new(p1.X - p2.X, p1.Y - p2.Y);

    public static Pos operator *(Pos p1, Pos p2) =>
        new(p1.X * p2.X, p1.Y * p2.Y);

    public static bool operator <=(Pos p1, Pos p2) =>
        p1.X <= p2.X && p1.Y <= p2.Y;

    public static bool operator >=(Pos p1, Pos p2) =>
        p1.X >= p2.X && p1.Y >= p2.Y;

    public bool Contains(Pos p2) =>
        X <= p2.X && Y <= p2.Y;

    public int ManhattanDistance(Pos that) =>
        Math.Abs(X - that.X) + Math.Abs(Y - that.Y);

    public IEnumerable<Pos> GetAxisOffsets() =>
        AxisOffsets.Select(pos => pos + new Pos(X, Y));

    public IEnumerable<Pos> GetDiagonalOffsets() =>
        DiagonalOffsets.Select(pos => pos + new Pos(X, Y));

    public IEnumerable<Pos> GetAllOffsets() =>
        GetAxisOffsets().Concat(GetDiagonalOffsets());
    
    public static Pos Zero = new(0, 0);

    private static readonly IEnumerable<Pos> AxisOffsets = new List<Pos>
    {
        new(-1, 0),
        new(0, -1),
        new(0, 1),
        new(1, 0),
    };

    private static readonly IEnumerable<Pos> DiagonalOffsets = new List<Pos>
    {
        new(-1, 1),
        new(1, 1),
        new(-1, -1),
        new(1, -1),
    };
}