﻿module fsharp._2024.Day6.Day6

open System.IO
open Xunit
open Faqt
open fsharp.Extensions

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

let findStartingPosition (nestedArray: char[][]) (value: char) =
    nestedArray
    |> Array.mapi (fun rowIndex row ->
        row
        |> Array.tryFindIndex (fun c -> c = value)
        |> Option.map (fun colIndex -> (rowIndex, colIndex)))
    |> Array.pick id // crash if not found

let safeAt (input: Array<Array<char>>) (row: int) (col: int) =
    if row >= 0 && row < input.Length && col >= 0 && col < input[row].Length then
        input[row].[col]
    else
        ' '

let isOutsideOfBounds (input: Array<Array<char>>) (row: int) (col: int) : bool =
    let h = input.Length
    let w = input[0].Length
    row < 0 || row > h - 1 || col < 0 || col > w - 1

let move ((x, y): int * int) (direction: Direction) : int * int =
    match direction with
    | Direction.Up -> x - 1, y
    | Direction.Right -> x, y + 1
    | Direction.Down -> x + 1, y
    | Direction.Left -> x, y - 1
    
[<Fact>]
let ``part1`` () =
    let lines = File.ReadAllLines "2024/Day6/Data.txt" |> Array.map _.ToCharArray()
    let mutable x, y = findStartingPosition lines '^'
    let mutable visitedPositions = Set.singleton (x,y)
    let mutable direction = Direction.Up
    let mutable continueLooping = true

    while continueLooping do
        visitedPositions <- visitedPositions.Add(x, y)

        let nextX, nextY = move (x,y) direction
        let nextSquare = safeAt lines nextX nextY

        match nextSquare with
        | '#' -> direction <- Direction.next direction
        | _ when isOutsideOfBounds lines nextX nextY -> continueLooping <- false
        | _ ->
            x <- nextX
            y <- nextY

    visitedPositions.Count.Should().Be(4374)

[<Fact>]
let ``part2`` () =
    let lines = File.ReadAllLines "2024/Day6/Data.txt" |> Array.map _.ToCharArray()

    // res.Should().Be(1888)
    ()