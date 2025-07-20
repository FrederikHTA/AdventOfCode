namespace csharp.csharp_lib.Graph;

public static class Dijkstra
{
    public static int GetShortestPath(int[,] heightmap, Pos.Pos start, Pos.Pos end)
    {
        var mapHeight = heightmap.GetLength(0);
        var mapWidth = heightmap.GetLength(1);

        var queue = new Queue<(Pos.Pos pos, int step)>();
        var visited = new HashSet<Pos.Pos>();

        //label root as explored
        //(start from S for part 1, and from any node of height value 1 for part 2)
        queue.Enqueue((start, 0));

        //while Q is not empty do
        while (queue.Any())
        {
            //v := Q.dequeue() - define the current node as the first in the queue
            var (pos, step) = queue.Dequeue();

            //(check if current node has been visited, if yes, skip exploration and adding to the queue)
            if (!visited.Add(pos))
                continue;

            //if v is the goal then - current node is end node, so break the loop
            if (pos.X == end.X && pos.Y == end.Y)
                //return v
                return step;

            //for all edges from v to w in G.adjacentEdges(v) do
            pos.GetAxisOffsets().ForEach(neighbour =>
            {
                //if w is not labeled as explored then
                //label w as explored
                if (neighbour.X >= 0 && neighbour.X < mapHeight && neighbour.Y >= 0 && neighbour.Y < mapWidth)
                {
                    //w.parent := v - define the child node and the current node as its parent
                    var parentNode = heightmap[pos.X, pos.Y];
                    var childNode = heightmap[neighbour.X, neighbour.Y];

                    //Q.enqueue(w)
                    if (childNode - parentNode <= 1)
                        queue.Enqueue((new Pos.Pos(neighbour.X, neighbour.Y), step + 1));
                }
            });
        }

        //no roots (starting node(s)), so no path
        return 0;
    }
}