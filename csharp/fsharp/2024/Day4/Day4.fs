module fsharp._2024.Day4.Day4

open System.IO
open Xunit
open Faqt
open fsharp.Extensions

let safeAt (input: Array<Array<char>>) (row: int) (col: int) =
    if row >= 0 && row < input.Length && col >= 0 && col < input[row].Length then
        input[row].[col]
    else
        ' '

let stringFromCharList (cl: List<char>) = String.concat "" <| List.map string cl

let getCharsForDirection (lines: Array<Array<char>>) (rowIndex: int) (colIndex: int) (rowDir: int) (colDir: int) =
    [ 1..3 ]
    |> List.map (fun n ->
        let row = (rowIndex + n * rowDir)
        let col = (colIndex + n * colDir)
        let res = safeAt lines row col
        res)
    |> stringFromCharList

[<Fact>]
let ``part1`` () =
    let lines = File.ReadAllLines "2024/Day4/Data.txt" |> Array.map _.ToCharArray()

    let directions =
        [ (-1, -1); (-1, 0); (-1, 1); (0, -1); (0, 1); (1, -1); (1, 0); (1, 1) ]

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
                            |> stringFromCharList

                        if sequence = "MAS" then 1 else 0)
                else
                    0)
            |> Seq.sum)
        |> Array.sum

    res.Should().Be(2514)

[<Fact>]
let ``part2`` () =
    let lines = File.ReadAllLines "2024/Day4/Data.txt" |> Array.map _.ToCharArray()

    let res =
        lines
        |> Array.mapi (fun rowIndex row ->
            row
            |> Array.mapi (fun colIndex cell ->
                if cell = 'A' then
                    let tl = safeAt lines (rowIndex - 1) (colIndex - 1)
                    let tr = safeAt lines (rowIndex - 1) (colIndex + 1)
                    let bl = safeAt lines (rowIndex + 1) (colIndex - 1)
                    let br = safeAt lines (rowIndex + 1) (colIndex + 1)
                    let a = System.String [| tl; br |]
                    let b = System.String [| bl; tr |]

                    if (a = "MS" || a = "SM") && (b = "MS" || b = "SM") then
                        1
                    else
                        0
                else
                    0)
            |> Array.sum)
        |> Array.sum

    res.Should().Be(1888)
