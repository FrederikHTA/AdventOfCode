module fsharp._2024.Day1.Day1

open System.IO
open System
open Xunit
open Faqt


let parseInput filePath =
    File.ReadLines filePath
    |> Seq.map (fun line ->
        match line.Split("   ") with
        | [| a; b |] -> (int a, int b)
        | _ -> failwith "Invalid input")
    |> Array.ofSeq

[<Fact>]
let ``part1`` () =
    let left, right = parseInput "2024/Day1/Data.txt" |> Array.unzip

    let left = left |> Seq.sort
    let right = right |> Seq.sort

    let sum =
        left |> Seq.zip right |> Seq.map (fun (x, y) -> Math.Abs(x - y)) |> Seq.sum

    sum.Should().Be(3246517)

[<Fact>]
let ``part2`` () =
    let numbers = parseInput "2024/Day1/Data.txt"

    let left = numbers |> Seq.map fst |> Seq.sort
    let countDict = numbers |> Seq.countBy snd |> Map.ofSeq

    let sum =
        left
        |> Seq.filter countDict.ContainsKey
        |> Seq.sumBy (fun x -> x * countDict[x])

    sum.Should().Be(29379307)
