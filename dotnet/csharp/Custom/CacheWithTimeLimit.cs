namespace csharp.Custom;

public class CacheWithTimeLimit
{
    private readonly int _expirationMs;

    // Per key, insertions in time order. Since Put times only increase,
    // expired entries are always a prefix, so a Queue lets Get evict them
    // in O(1) amortized instead of scanning the whole list.
    private readonly Dictionary<int, Queue<(long InsertionTime, int Value)>> _cache = new();

    public CacheWithTimeLimit(int expirationMs)
    {
        _expirationMs = expirationMs;
    }

    public void Put(int key, int value, long insertionTimeMs)
    {
        if (!_cache.TryGetValue(key, out var queue))
        {
            queue = new Queue<(long, int)>();
            _cache[key] = queue;
        }

        queue.Enqueue((insertionTimeMs, value));
    }

    // Returns every value for `key` that hasn't expired as of `currentTimeMs`.
    public List<int> Get(int key, long currentTimeMs)
    {
        if (!_cache.TryGetValue(key, out var queue))
            return [];

        while (queue.Count > 0 && queue.Peek().InsertionTime + _expirationMs <= currentTimeMs)
            queue.Dequeue();

        return queue.Select(entry => entry.Value).ToList();
    }
}

public class Runner
{
    public static void Run()
    {
        var cache = new CacheWithTimeLimit(expirationMs: 300);

        cache.Put('A', 1, 100);
        cache.Put('A', 2, 300);
        cache.Put('A', 3, 500);

        var result = cache.Get('A', 500);
        Console.WriteLine($"[{string.Join(", ", result)}]");

        if (!result.SequenceEqual([2, 3]))
            throw new Exception($"expected [2, 3] but got [{string.Join(", ", result)}]");
    }
}
