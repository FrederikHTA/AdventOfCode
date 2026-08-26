namespace csharp.LeetCode;

public class LRUCache
{
    // https://leetcode.com/problems/lru-cache/
    // LinkedList front = most recently used, back = least recently used.
    private readonly LinkedList<(int Key, int Value)> _order = [];
    private readonly Dictionary<int, LinkedListNode<(int Key, int Value)>> _cache = new();
    private readonly int _capacity;

    public LRUCache(int capacity)
    {
        _capacity = capacity;
    }

    private int Get(int key)
    {
        if (!_cache.TryGetValue(key, out var node))
            return -1;

        _order.Remove(node);
        _order.AddFirst(node);
        return node.Value.Value;
    }

    private void Put(int key, int value)
    {
        if (_cache.TryGetValue(key, out var existing))
        {
            _order.Remove(existing);
        }
        else if (_cache.Count >= _capacity)
        {
            var lru = _order.Last!;
            _cache.Remove(lru.Value.Key);
            _order.RemoveLast();
        }

        var node = new LinkedListNode<(int Key, int Value)>((key, value));
        _order.AddFirst(node);
        _cache[key] = node;
    }
    
    
    public static void Run()
    {
        string[] operations = ["LRUCache", "put", "put", "get", "put", "get", "put", "get", "get", "get"];
        int[][] values = [[2], [1, 1], [2, 2], [1], [3, 3], [2], [4, 4], [1], [3], [4]];

        var capacity = values[0][0];
        var cache = new LRUCache(capacity);
        Console.Write("null, ");
        for (var i = 1; i < operations.Length; i++)
        {
            var operation = operations[i];

            if (operation == "put")
            {
                var (head, tail) = (values[i].First(), values[i].Last());
                cache.Put(head, tail);
                Console.Write("null, ");
            }
            else // get
            {
                var value = values[i].First();
                var fetchedValue = cache.Get(value);
                Console.Write($"{fetchedValue}, ");
            }
        }
    }
}