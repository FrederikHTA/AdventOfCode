module fsharp._2025.Day2.Day2

open System.IO
open System
open System.Text.RegularExpressions
open Xunit

let parseInput filePath =
    let lines = File.ReadLines filePath
    lines |> Seq.map (fun line -> (line[0], int line[1..])) |> Seq.toArray

[<Fact>]
let ``part1`` () =
    let input = parseInput "2025/Day2/Data.txt"

    ()
// counter.Should().Be 1007

[<Fact>]
let ``part2`` () =
    let input = parseInput "2025/Day2/Data.txt"

    ()
// counter.Should().Be 1007
