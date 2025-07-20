module fsharp._2024.Day2.Day2

open System.IO
open Xunit
open Faqt
open fsharp.Extensions

[<RequireQualifiedAccess>]
type Direction =
    | Increasing
    | Decreasing

let parseInput filePath =
    File.ReadLines filePath |> Seq.map (fun line -> line.Split (" ") |> Array.map int) |> Array.ofSeq

let isSafe (line : Array<int>) =
    let direction = if line[0] < line[1] then Direction.Increasing else Direction.Decreasing

    let directionDiffFunc =
        match direction with
        | Direction.Increasing -> fun a b -> b - a > 0 && b - a <= 3
        | Direction.Decreasing -> fun a b -> a - b > 0 && a - b <= 3

    line |> Array.pairwise |> Array.forall (fun (a, b) -> not (a = b) && directionDiffFunc a b)

[<Fact>]
let ``part1`` () =
    let safeLines = parseInput "2024/Day2/Data.txt" |> Array.filter isSafe |> Array.length

    safeLines.Should().Be (379)

[<Fact>]
let ``part2`` () =
    let safeLines =
        parseInput "2024/Day2/Data.txt"
        |> Array.map (fun row -> row |> Array.mapi (fun i _ -> row |> Array.removeAt i |> isSafe))
        |> Array.filter (fun row -> row |> Array.exists id)
        |> Array.length

    safeLines.Should().Be (430)
