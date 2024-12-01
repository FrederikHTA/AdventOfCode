module fsharp._2024.Day1.Day1

open System
open System.IO

let getFileLines filePath =
    let fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath)
    File.ReadLines(fullPath)

let parseInput filePath =
    getFileLines filePath
    |> Seq.map (fun line ->
        let parts = line.Split("   ")
        int parts[0], int parts[1])

let part1 =
    let intNumbers = parseInput "/2024/Day1/Data.txt"

    let left = intNumbers |> Seq.map fst |> Seq.sort
    let right = intNumbers |> Seq.map snd |> Seq.sort

    let result =
        Seq.zip left right |> Seq.map (fun (x, y) -> Math.Abs(x - y)) |> Seq.sum

    assert (result = 3246517)

let part2 =
    let intNumbers = parseInput "/2024/Day1/Data.txt"

    let left = intNumbers |> Seq.map fst |> Seq.sort

    let countDict = intNumbers |> Seq.countBy snd |> Map.ofSeq

    let result =
        left
        |> Seq.filter countDict.ContainsKey
        |> Seq.sumBy (fun x -> x * countDict[x])

    assert (result = 29379307)

part1
part2
