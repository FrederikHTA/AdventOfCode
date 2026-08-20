namespace ConsoleApp1
{
    // Input
    // ["TimeMap", "set", "get", "get", "set", "get", "get"]
    // [[], ["foo", "bar", 1], ["foo", 1], ["foo", 3], ["foo", "bar2", 4], ["foo", 4], ["foo", 5]]
    // Output
    // [null, null, "bar", "bar", null, "bar2", "bar2"]

    // Explanation
    // TimeMap timeMap = new TimeMap();
    // timeMap.set("foo", "bar", 1);  // store the key "foo" and value "bar" along with timestamp = 1.
    // timeMap.get("foo", 1);         // return "bar"
    // timeMap.get("foo", 3);         // return "bar", since there is no value corresponding to foo at timestamp 3 and timestamp 2, then the only value is at timestamp 1 is "bar".
    // timeMap.set("foo", "bar2", 4); // store the key "foo" and value "bar2" along with timestamp = 4.
    // timeMap.get("foo", 4);         // return "bar2"
    // timeMap.get("foo", 5);         // return "bar2"
    public class TimeMap
    {
         private readonly Dictionary<string, List<(int Timestamp, string Value)>> _map;

        public TimeMap()
        {
            _map = [];
        }

        public void Set(string key, string value, int timestamp)
        {
            if (!_map.TryGetValue(key, out var value1))
            {
                value1 = [];
                _map[key] = value1;
            }

            value1.Add((timestamp, value));
        }

        public string Get(string key, int timestamp)
        {
            if (!_map.TryGetValue(key, out var list) || list.Count == 0)
            {
                return "";
            }

            var index = BinarySearch(list, timestamp);
            return index < 0 ? "" : list[index].Value;
        }
        
        private static int BinarySearch(List<(int Timestamp, string Value)> list, int timestamp)
        {
            var left = 0;
            var right = list.Count - 1;
            while (left <= right)
            {
                var mid = left + (right - left) / 2;
                if (list[mid].Timestamp == timestamp)
                {
                    return mid;
                }

                if (list[mid].Timestamp < timestamp)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return right;
        }
    }
}

