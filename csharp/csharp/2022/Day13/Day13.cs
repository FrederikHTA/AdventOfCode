using System.Text.Json;
using FluentAssertions;

namespace _2022_csharp.Day13;

record PacketPair(JsonElement Left, JsonElement Right);

static class Day13
{
    public static void Part1()
    {
        var input = File.ReadAllText("./Day13/Data.txt");

        var packetPairs = ParseInput(input)
            .Select(x => new PacketPair(
                JsonSerializer.Deserialize<JsonElement>(x[0]),
                JsonSerializer.Deserialize<JsonElement>(x[1])))
            .ToList();

        var result = packetPairs
            .Select((packetPair, index) => new
            {
                ComparisonValue = ComparePackets(packetPair.Left, packetPair.Right),
                Index = index + 1
            })
            .Where(x => x.ComparisonValue < 0)
            .Sum(y => y.Index);

        result.Should().Be(6240);
    }

    public static void Part2()
    {
        var input = File.ReadAllText("./Day13/Data.txt");

        var dividerPackets = new List<string> { "[[2]]", "[[6]]" };
        var packetPairs = ParseInput(input)
            .SelectMany(x => x)
            .Concat(dividerPackets)
            .ToList();

        packetPairs.Sort((x, y) => ComparePackets(
            JsonSerializer.Deserialize<JsonElement>(x),
            JsonSerializer.Deserialize<JsonElement>(y)));

        var divider1 = packetPairs.IndexOf(dividerPackets[0]) + 1;
        var divider2 = packetPairs.IndexOf(dividerPackets[1]) + 1;
        var result = divider1 * divider2;

        result.Should().Be(23142);
    }

    private static int ComparePackets(JsonElement left, JsonElement right)
    {
        return (left.ValueKind, right.ValueKind) switch
        {
            (JsonValueKind.Number, JsonValueKind.Number) =>
                left.GetInt32() - right.GetInt32(),
            (JsonValueKind.Number, _) =>
                CompareLists(ConvertToJsonList(left), right),
            (_, JsonValueKind.Number) =>
                CompareLists(left, ConvertToJsonList(right)),
            _ => CompareLists(left, right),
        };

        JsonElement ConvertToJsonList(JsonElement x) =>
            JsonSerializer.Deserialize<JsonElement>($"[{x.GetInt32()}]");
    }

    private static int CompareLists(JsonElement left, JsonElement right)
    {
        var e1 = left.EnumerateArray();
        var e2 = right.EnumerateArray();
        while (e1.MoveNext() && e2.MoveNext())
        {
            var res = ComparePackets(e1.Current, e2.Current);
            if (res != 0)
            {
                return res;
            }
        }

        return left.GetArrayLength() - right.GetArrayLength();
    }

    private static IEnumerable<string[]> ParseInput(string input)
    {
        return input
            .Split("\r\n\r\n")
            .Select(x => x.Split("\r\n"))
            .ToList();
    }
}