module fsharp._2024.Day3.Day3

open System.IO
open System
open System.Text.RegularExpressions
open Xunit
open Faqt

let parseInput (filePath: string) : string = File.ReadAllText filePath

let extractMultiples (matches : seq<Match>) =
    matches
    |> Seq.cast<Match>
    |> Seq.map (fun m -> int m.Groups.[1].Value * int m.Groups.[2].Value)
    |> Seq.sum

[<Fact>]
let ``part1`` () =
    let lines = parseInput "2024/Day3/Data.txt"
    let regex = Regex("""mul\((\d+),(\d+)\)""")
    let res = extractMultiples (regex.Matches lines)
    res.Should().Be(184122457)

[<Fact>]
let ``part2`` () =
    let lines = parseInput "2024/Day3/Data.txt"
    let regex = Regex("""mul\((\d+),(\d+)\)|do\(\)|don't\(\)""")

    let mutable flag = true
    let filtered =
        regex.Matches(lines)
        |> Seq.cast<Match>
        |> Seq.filter (fun m ->
            match m.Value with
            | "do()" -> flag <- true; false
            | "don't()" -> flag <- false; false
            | _ -> flag)

    let res = extractMultiples filtered
    res.Should().Be(107862689)

