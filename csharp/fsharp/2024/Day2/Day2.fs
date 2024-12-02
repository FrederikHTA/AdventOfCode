module fsharp._2024.Day2.Day2

open System.IO
open System
open Xunit
open fsharp.FileUtility

[<RequireQualifiedAccess>]
type Direction =
    | Increasing
    | Decreasing

let parseInput filePath =
    getFileLines filePath
    |> Seq.map (fun line -> line.Split(" ") |> Array.map int)
    |> Array.ofSeq

let isSafe (line: int array) =
    let direction =
        if line[0] < line[1] then
            Direction.Increasing
        else
            Direction.Decreasing

    let directionDiffFunc =
        match direction with
        | Direction.Increasing -> fun a b -> b - a > 0 && b - a <= 3
        | Direction.Decreasing -> fun a b -> a - b > 0 && a - b <= 3

    line
    |> Array.pairwise
    |> Array.forall (fun (a, b) -> not (a = b) && directionDiffFunc a b)

[<Fact>]
let ``part1`` () =
    let safeLines =
        parseInput "2024/Day2/Data.txt" |> Array.filter isSafe |> Array.length

    assert (safeLines = 379)

[<Fact>]
let ``part2`` () =
    let result =
        parseInput "2024/Day2/Data.txt"
        |> Array.map (fun row ->
            row
            |> Array.mapi (fun i _ ->
                let rowIter = row |> Array.removeAt i
                rowIter |> isSafe))
        |> Array.filter (fun row -> row |> Array.exists id)
        |> Array.length

    assert (result = 4)
