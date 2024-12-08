module fsharp._2024.Day4.Day4

open System.IO
open Xunit
open Faqt
open fsharp.Extensions
open fsharp.Grid
open fsharp.Pos

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
    let grid =
        File.ReadAllLines "2024/Day4/Data.txt"
        |> Array.map _.ToCharArray()
        |> Grid.create

    let res =
        grid
        |> Array.mapi (fun rowIndex row ->
            row
            |> Array.mapi (fun colIndex cell ->
                let pos = Pos.create rowIndex colIndex

                if cell = 'X' then
                    Pos.allOffsets
                    |> List.sumBy (fun offset ->
                        let sequence =
                            [ 1..3 ]
                            |> List.map (fun n ->
                                let newPos = pos + Pos.create (n * offset.X) (n * offset.Y)
                                grid |> Grid.tryGet newPos ' ')
                            |> stringFromCharList

                        if sequence = "MAS" then 1 else 0)
                else
                    0)
            |> Array.sum)
        |> Array.sum

    res.Should().Be(2514)

[<Fact>]
let ``part2`` () =
    let grid = File.ReadAllLines "2024/Day4/Data.txt" |> Array.map _.ToCharArray()

    let res =
        grid
        |> Array.mapi (fun rowIndex row ->
            row
            |> Array.mapi (fun colIndex cell ->
                let pos = { X = rowIndex; Y = colIndex }

                if cell = 'A' then
                    let offsetValues =
                        pos
                        |> Pos.getDiagonalOffsets
                        |> List.map (fun offset -> grid |> Grid.tryGet offset ' ')

                    let x1, x2 =
                        match offsetValues with
                        | [ tl; tr; bl; br ] -> (System.String [| tl; br |], System.String [| bl; tr |])
                        | _ -> failwith "Invalid"

                    if (x1 = "MS" || x1 = "SM") && (x2 = "MS" || x2 = "SM") then
                        1
                    else
                        0
                else
                    0)
            |> Array.sum)
        |> Array.sum

    res.Should().Be(1888)
