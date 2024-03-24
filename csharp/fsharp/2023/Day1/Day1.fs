module fsharp.Day1

open System.Text.RegularExpressions

let regex = Regex("\d")

let fixInvalidNumbers (s: string) =
    s
        .Replace("one", "o1e")
        .Replace("two", "t2o")
        .Replace("three", "th3e")
        .Replace("four", "4")
        .Replace("five", "5e")
        .Replace("six", "6")
        .Replace("seven", "7n")
        .Replace("eight", "e8t")
        .Replace("nine", "n9e")

let findNumbers (input: string) (regex: Regex) =
    let matches = regex.Matches(input)
    let firstMatch = matches.Item(0).Value
    let lastMatch = matches.Item(matches.Count - 1).Value

    int $"{firstMatch}{lastMatch}"

let input = System.IO.File.ReadAllLines "2023/Day1/Data.txt"

let part1Res = input |> Seq.map (fun x -> findNumbers x regex) |> Seq.sum
assert (part1Res = 55029)

let part2Res =
    input
    |> Seq.map fixInvalidNumbers
    |> Seq.map (fun x -> findNumbers x regex)
    |> Seq.sum

assert (part2Res = 55686)
