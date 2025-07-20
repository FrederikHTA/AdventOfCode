using System.Text.RegularExpressions;
using csharp.csharp_lib;
using FluentAssertions;

namespace csharp._2023.Day8;

static class Day8
{
    public static void Part1()
    {
        var (directions, nodes) = ParseInput("/2023/Day8/TestData.txt");

        var steps = 0;
        var current = "AAA";
        for (var i = 0; i < directions.Length; i++)
        {
            var direction = directions[i];
            current = direction switch
            {
                'R' => nodes[current].Item2,
                'L' => nodes[current].Item1,
                _ => current
            };

            steps++;

            if (current == "ZZZ")
            {
                break;
            }

            if (i == directions.Length - 1)
            {
                i = -1;
            }
        }

        steps.Should().Be(16271);
    }

    public static void Part2()
    {
        var (directions, nodes) = ParseInput("/2023/Day8/Data.txt");
        var ghostsStartPosition = nodes.Keys.Where(x => x.EndsWith('A')).ToList();

        var res = new List<long>();
        foreach (var t in ghostsStartPosition)
        {
            var steps = 0;
            var current = t;
            for (var i = 0; i < directions.Length; i++)
            {
                if (current.EndsWith('Z'))
                {
                    res.Add(steps);
                    break;
                }

                steps++;
                var direction = directions[i];
                current = direction switch
                {
                    'R' => nodes[current].Item2,
                    'L' => nodes[current].Item1,
                    _ => current
                };
                
                if (i == directions.Length - 1)
                {
                    i = -1;
                }
            }
        }

        var lcm = res.Aggregate(Lcm);
        lcm.Should().Be(14_265_111_103_729);
    }

    private static (char[] Directions, Dictionary<string, (string, string)>) ParseInput(string path)
    {
        var input = Utilities.GetLines(path).ToList();
        var regex = new Regex(@"(\w{3})");
        var nodes = new Dictionary<string, (string, string)>();
        input.Skip(2).ForEach(x =>
        {
            var split = regex.Matches(x);
            nodes.Add(split[0].Value, (split[1].Value, split[2].Value));
        });

        var directions = input.First().ToCharArray();
        return (directions, nodes);
    }

    private static long Lcm(long a, long b)
    {
        return a / Gcf(a, b) * b;
    }

    private static long Gcf(long a, long b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}