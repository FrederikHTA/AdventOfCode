module fsharp._2024.Day6.Day6

open System.IO
open Xunit
open Faqt
open fsharp
open fsharp.Extensions
open fsharp.Pos
open fsharp.Grid

[<RequireQualifiedAccess>]
type Direction =
    | Up
    | Down
    | Left
    | Right

module Direction =
    let next (direction: Direction) =
        match direction with
        | Direction.Up -> Direction.Right
        | Direction.Right -> Direction.Down
        | Direction.Down -> Direction.Left
        | Direction.Left -> Direction.Up

let safeAt (input: Array<Array<char>>) (row: int) (col: int) =
    if row >= 0 && row < input.Length && col >= 0 && col < input[row].Length then
        input[row].[col]
    else
        ' '

let isOutsideOfBounds (input: Array<Array<char>>) (row: int) (col: int) : bool =
    let h = input.Length
    let w = input[0].Length
    row < 0 || row > h - 1 || col < 0 || col > w - 1

let move (pos: Pos) (direction: Direction) : Pos =
    match direction with
    | Direction.Up -> { X = pos.X - 1; Y = pos.Y }
    | Direction.Right -> { X = pos.X; Y = pos.Y + 1 }
    | Direction.Down -> { X = pos.X + 1; Y = pos.Y }
    | Direction.Left -> { X = pos.X; Y = pos.Y - 1 }

[<Fact>]
let ``part1`` () =
    let lines =
        File.ReadAllLines "2024/Day6/Data.txt"
        |> Array.map _.ToCharArray()
        |> Grid.create

    let mutable currentPos = lines |> Grid.getPosOf '^'
    let mutable visitedPositions = Set.singleton currentPos
    let mutable direction = Direction.Up
    let mutable continueLooping = true

    while continueLooping do
        visitedPositions <- visitedPositions.Add currentPos

        let nextPos = move currentPos direction
        let nextSquare = lines |> Grid.tryGet nextPos ' '

        match nextSquare with
        | '#' -> direction <- Direction.next direction
        | _ when lines |> Grid.containsPos nextPos |> not -> continueLooping <- false
        | _ -> currentPos <- nextPos

    visitedPositions.Count.Should().Be(4374)

[<Fact>]
let ``part2`` () =
    let lines = File.ReadAllLines "2024/Day6/TestData.txt" |> Array.map _.ToCharArray()

    // res.Should().Be(1888)
    ()
