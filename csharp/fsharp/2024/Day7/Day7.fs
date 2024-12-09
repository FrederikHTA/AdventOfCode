module fsharp._2024.Day7.Day7

open System.IO
open Xunit
open Faqt
open fsharp
open fsharp.Extensions
open fsharp.Pos
open fsharp.Grid

let readInput (path: string): Array<float * Array<float>> =
    File.ReadAllLines path
    |> Array.map (fun x ->
        let split = x.Split(":")
        let left = float split.[0]
        let right = split.[1].Trim().Split(" ") |> Array.map float
        (left, right))
    
[<Fact>]
let ``part1`` () =
    let lines = readInput "2024/Day7/Data.txt"
    let mutable results = []

    for target, operators in lines do
        let mutable rowResult = []

        for operator in operators do
            if rowResult.IsEmpty then
                rowResult <- [ operator ]
            else
                let mutable resultsToAdd = []

                for result in rowResult do
                    let add = operator + result
                    let mul = operator * result

                    if add <= target then
                        resultsToAdd <- add :: resultsToAdd

                    if mul <= target then
                        resultsToAdd <- mul :: resultsToAdd

                rowResult <- resultsToAdd

        let isMatch = rowResult |> List.tryFind (fun x -> x = target)

        if isMatch.IsSome then
            results <- isMatch.Value :: results
            
    results |> List.sum |> float32 |> _.Should().Be(1708857123053f)


[<Fact>]
let ``part2`` () =
    let lines = readInput "2024/Day7/Data.txt"
    let mutable results = []

    for target, operators in lines do
        let mutable rowResult = []

        for operator in operators do
            if rowResult.IsEmpty then
                rowResult <- [ operator ]
            else
                let mutable resultsToAdd = []

                for result in rowResult do
                    let add = operator + result
                    let mul = operator * result
                    let concat = (string result + string operator) |> float

                    if add <= target then
                        resultsToAdd <- add :: resultsToAdd

                    if mul <= target then
                        resultsToAdd <- mul :: resultsToAdd
                        
                    if concat <= target then
                        resultsToAdd <- concat :: resultsToAdd

                rowResult <- resultsToAdd

        let isMatch = rowResult |> List.tryFind (fun x -> x = target)

        if isMatch.IsSome then
            results <- isMatch.Value :: results
            
    results |> List.sum |> float32 |> _.Should().Be(189207836795655f)
