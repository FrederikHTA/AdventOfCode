using System.Text.RegularExpressions;
using _2022_csharp.Lib;
using FluentAssertions;

class Monkey
{
    public List<int> Items;
    public string Operation;
    public int TestNumber;
    public int TrueMonkey;
    public int FalseMonkey;

    private int _inspections;
    private int _worryLevel;

    public Monkey(
        List<int> items,
        string operation,
        int testNumber,
        int trueMonkey,
        int falseMonkey)
    {
        Items = items;
        Operation = operation;
        TestNumber = testNumber;
        TrueMonkey = trueMonkey;
        FalseMonkey = falseMonkey;
    }

    public (int worryLevel, int newMonkey) Inspect1()
    {
        _inspections++;
        _worryLevel = Items[0];
        Items.RemoveAt(0);
        var operationResult = RunOperation();
        var newWorryLevel = (int)Math.Floor((decimal)operationResult / 3);

        return newWorryLevel % TestNumber == 0
            ? (newWorryLevel, TrueMonkey)
            : (newWorryLevel, FalseMonkey);
    }

    public (int worryLevel, int newMonkey) Inspect2()
    {
        _inspections++;
        _worryLevel = Items[0];
        Items.RemoveAt(0);
        var operationResult = RunOperation();

        return operationResult % TestNumber == 0
            ? (operationResult, TrueMonkey)
            : (operationResult, FalseMonkey);
    }

    public int GetInspections() => _inspections;

    private int RunOperation()
    {
        var operation = Operation.Split(" ");
        var left = operation[0] == "old" ? _worryLevel : int.Parse(operation[0]);
        var right = operation[2] == "old" ? _worryLevel : int.Parse(operation[2]);

        var result = operation[1] == "+"
            ? left + right
            : left * right;

        return result;
    }
}

static class Day11
{
    public static void Part1()
    {
        var monkeys = ParseToMonkey();

        Enumerable.Range(1, 20).ForEach(_ =>
        {
            foreach (var monkey in monkeys)
            {
                var count = monkey.Items.Count;
                for (var i = 0; i < count; i++)
                {
                    var (worryLevel, newMonkey) = monkey.Inspect1();
                    monkeys[newMonkey].Items.Add(worryLevel);
                }
            }
        });

        var monkeyBusiness = CalculateMonkeyBusiness(monkeys);

        monkeyBusiness.Should().Be(56120);
        Console.WriteLine("Result: " + monkeyBusiness);
    }

    public static void Part2()
    {
        var monkeys = ParseToMonkey();

        Enumerable.Range(1, 10000).ForEach(roundNumber =>
        {
            foreach (var monkey in monkeys)
            {
                var count = monkey.Items.Count;
                for (var i = 0; i < count; i++)
                {
                    var (worryLevel, newMonkey) = monkey.Inspect2();
                    monkeys[newMonkey].Items.Add(worryLevel);
                }
            }
            var round = roundNumber;

        });

        var monkeyBusiness = CalculateMonkeyBusiness(monkeys);

        // 2088394607 -- too low
        // monkeyBusiness.Should().Be(56120);
        Console.WriteLine("Result: " + monkeyBusiness);
    }

    private static int CalculateMonkeyBusiness(List<Monkey> monkeys)
    {
        return monkeys
            .Select(x => x.GetInspections())
            .OrderByDescending(x => x)
            .Take(2)
            .Aggregate((x, y) => x * y);
    }

    private static List<Monkey> ParseToMonkey()
    {
        return Utilities.GetLines("/Day11/Data.txt")
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x => x.Trim())
            .Chunk(6)
            .Select(x =>
            {
                var startingItems = x[1]
                    .Split(":")[1]
                    .Split(",")
                    .Select(int.Parse)
                    .ToList();
                var operation = x[2].Split("=")[1].Trim();
                var testNumber = int.Parse(x[3].Split("by ")[1]);
                var trueMonkey = int.Parse(x[4].Split("monkey ")[1]);
                var falseMonkey = int.Parse(x[5].Split("monkey ")[1]);

                return new Monkey(startingItems, operation, testNumber, trueMonkey, falseMonkey);
            }).ToList();
    }
}