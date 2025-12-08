module fsharp._2025.Day6.Day6

open System
open System.IO
open System.Text.RegularExpressions
open Xunit
open Expecto

let parseInput1 filePath =
    let lines = File.ReadAllLines filePath

    lines
    |> Array.map (fun line ->
        line.Trim().Split ' ' |> Array.filter (fun x -> x |> String.IsNullOrWhiteSpace |> not)
    )
    |> Array.transpose
    |> Array.map (fun x ->
        let tillLast = x[.. x.Length - 2] |> Array.map bigint.Parse
        let operator = x |> Array.last
        tillLast, operator |> char
    )

[<Fact>]
let ``part1`` () =
    let input = parseInput1 "2025/Day6/Data.txt"

    input
    |> Array.map (fun (numbers, operator) ->
        match operator with
        | '+' -> numbers |> Array.sum
        | '*' -> numbers |> Array.reduce (*)
        | _ -> failwith "Invalid operator"
    )
    |> Array.sum
    |> Flip.Expect.equal "equal" 5346286649122I

let parseInput2 filePath =
    let regex = Regex ("""\w+(?=\s\d)""", RegexOptions.Compiled)
    let lines = File.ReadAllLines filePath

    let matches = lines |> Array.map (fun line -> regex.Matches line )
    // |> Array.transpose
    // |> Array.map (fun x ->
    //     let tillLast = x[.. x.Length - 2] |> Array.map bigint.Parse
    //     let operator = x |> Array.last
    //     tillLast, operator |> char
    // )

    [|[||], 'c'|]


[<Fact>]
let ``part2`` () =
    let input = parseInput2 "2025/Day6/TestData.txt"

    input
    |> Array.map (fun (numbers, operator) ->
        match operator with
        | '+' -> numbers |> Array.sum
        | '*' -> numbers |> Array.reduce (*)
        | _ -> failwith "Invalid operator"
    )
    |> Array.sum
    |> Flip.Expect.equal "equal" 3263827I
