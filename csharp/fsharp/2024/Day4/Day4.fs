module fsharp._2024.Day4.Day4

open System.IO
open Xunit
open Faqt
open Xunit.Abstractions
open fsharp.Extensions

let directions =
    [ (-1, -1); (-1, 0); (-1, 1); (0, -1); (0, 1); (1, -1); (1, 0); (1, 1) ]

let safeAt (input: Array<Array<char>>) (row: int) (col: int) =
    if row >= 0 && row < input.Length && col >= 0 && col < input[row].Length then
        input[row].[col]
    else
        ' '

let getCharsForDirection (lines: Array<Array<char>>) (rowIndex: int) (colIndex: int) (rowDir: int) (colDir: int) : char list =
    [ 1..3 ]
    |> List.map (fun n ->
        let row = (rowIndex + n * rowDir)
        let col = (colIndex + n * colDir)
        let res = safeAt lines row col
        res)
    |> Seq.toList

[<AutoOpen>]
type Tests(output: ITestOutputHelper) =
    [<Fact>]
    let ``part1`` () =
        let lines = File.ReadAllLines "2024/Day4/Data.txt" |> Array.map _.ToCharArray()

        let res =
            lines
            |> Array.mapi (fun rowIndex row ->
                row
                |> Array.mapi (fun colIndex cell ->
                    if cell = 'X' then
                        directions
                        |> List.sumBy (fun (rowDir, colDir) ->
                            let sequence = getCharsForDirection lines rowIndex colIndex rowDir colDir
                            if sequence = [ 'M'; 'A'; 'S' ] then 1 else 0)
                    else
                        0)
                |> Seq.sum)
            |> Array.sum

        res.Should().Be(2514)

    [<Fact>]
    let ``part2`` () =
        let lines = File.ReadAllLines "2024/Day4/TestData.txt" |> Array.map _.ToCharArray()

        let res =
            lines
            |> Array.mapi (fun rowIndex row ->
                row
                |> Array.mapi (fun colIndex cell ->
                    if cell = 'X' then
                        directions
                        |> List.sumBy (fun (rowDir, colDir) ->
                            let sequence =
                                [ 1..3 ]
                                |> List.map (fun n ->
                                    let row = (rowIndex + n * rowDir)
                                    let col = (colIndex + n * colDir)
                                    let res = safeAt lines row col
                                    res)
                                |> Seq.toList

                            if sequence = [ 'M'; 'A'; 'S' ] then 1 else 0)
                    else
                        0)
                |> Seq.sum)
            |> Array.sum

        res.Should().Be(2514)
