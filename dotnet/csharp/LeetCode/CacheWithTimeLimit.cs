namespace csharp.LeetCode;

public class CacheWithTimeLimit
{
    private readonly int _expirationMs;
    private readonly Dictionary<int, Queue<(long InsertionTime, int Value)>> _cache = new();

    // Global insertion order across all keys, so a sweep on ANY call can
    // evict expired entries even for keys nobody has Get-ed lately.
    // Without this, a key that's only ever Put (never Get) grows forever.
    private readonly Queue<(long InsertionTime, int Key)> _order = new();

    public CacheWithTimeLimit(int expirationMs)
    {
        _expirationMs = expirationMs;
    }

    // Total live entries across all keys - bounded by what's within the
    // expiration window, not by how many Put calls ever happened.
    public int Count => _order.Count;

    public void Put(int key, int value, long insertionTimeMs)
    {
        EvictExpired(insertionTimeMs);

        if (!_cache.TryGetValue(key, out var queue))
        {
            queue = new Queue<(long, int)>();
            _cache[key] = queue;
        }

        queue.Enqueue((insertionTimeMs, value));
        _order.Enqueue((insertionTimeMs, key));
    }

    // Returns every value for `key` that hasn't expired as of `currentTimeMs`.
    public List<int> Get(int key, long currentTimeMs)
    {
        EvictExpired(currentTimeMs);

        if (_cache.TryGetValue(key, out var queue))
        {
            return queue.Select(entry => entry.Value).ToList();
        }

        return [];
    }

    private void EvictExpired(long currentTimeMs)
    {
        while (_order.Count > 0 && _order.Peek().InsertionTime + _expirationMs <= currentTimeMs)
        {
            var (_, key) = _order.Dequeue();
            var queue = _cache[key];
            queue.Dequeue();

            if (queue.Count == 0)
                _cache.Remove(key);
        }
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

        // 'B' is only ever Put, never Get - proves eviction isn't tied to
        // reading a key: a later Put on 'A' alone must still sweep 'B' out.
        cache.Put('B', 99, 100);
        cache.Put('A', 4, 900); // sweeps everything with InsertionTime + 300 <= 900

        if (cache.Count != 1)
            throw new Exception($"expected only 'A'@4 to survive but Count was {cache.Count}");
    }
}
