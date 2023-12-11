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
    
    public Grid<T> Transpose()
    {
        var newData = new List<List<T>>();
        for (var i = 0; i < Width; i++)
        {
            newData.Add([]);
            for (var j = 0; j < Height; j++)
            {
                newData[i].Add(Data[j][i]);
            }
        }

        return new Grid<T>(newData);
    }
    
    public void Visualize()
    {
        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                Console.Write(data[i][j]);
            }
            Console.WriteLine();
        }

    }

    public int Width => Data[0].Count;
    public int Height => Data.Count;
}