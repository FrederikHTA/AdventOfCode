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
    
let solve (supportsOr: bool) (lines: Array<float * Array<float>>) : float list =
    let mutable equationsThatMatchTarget = []

    for target, operators in lines do
        let mutable rowResult = []

        for operator in operators do
            if rowResult.IsEmpty then
                rowResult <- [ operator ]
            else
                let mutable operatorResults = []

                for result in rowResult do
                    let add = operator + result
                    let mul = operator * result

                    if add <= target then
                        operatorResults <- add :: operatorResults

                    if mul <= target then
                        operatorResults <- mul :: operatorResults
                        
                    if supportsOr then
                        let concat = (string result + string operator) |> float
                        if concat <= target then
                            operatorResults <- concat :: operatorResults
                    else ()

                rowResult <- operatorResults

        let isMatch = rowResult |> List.tryFind (fun x -> x = target)

        if isMatch.IsSome then
            equationsThatMatchTarget <- isMatch.Value :: equationsThatMatchTarget
    equationsThatMatchTarget 
    
[<Fact>]
let ``part1`` () =
    let lines = readInput "2024/Day7/Data.txt"
    let equationsThatMatchTarget = lines |> solve false
    equationsThatMatchTarget |> List.sum |> float32 |> _.Should().Be(1708857123053f)

[<Fact>]
let ``part2`` () =
    let lines = readInput "2024/Day7/Data.txt"
    let equationsThatMatchTarget = lines |> solve true
    equationsThatMatchTarget |> List.sum |> float32 |> _.Should().Be(189207836795655f)
