namespace csharp.csharp_lib.Grid;

public class Grid<T>(List<List<T>> data)
{
    public List<List<T>> Data { get; } = data;

    public T Get(Pos.Pos pos)
    {
        return Data[pos.X][pos.Y];
    }

    public bool ContainsPos(Pos.Pos pos)
    {
        return pos.X >= 0 && pos.X < Data.Count && pos.Y >= 0 && pos.Y < Data[0].Count;
    }

    public int Width => Data[0].Count;
    public int Height => Data.Count;
}