module Program

open System.IO
open fsharp.Extensions

// [<EntryPoint>]
// let main _ = 0

[<RequireQualifiedAccess>]
type Direction =
    | Up
    | Down
    | Left
    | Right

module Direction =
    let getNextDirection (direction: Direction) =
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

    if row < 0 || row > h-1 || col < 0 || col > w-1 then
        true
    else
        false
let lines = File.ReadAllLines "2024/Day6/Data.txt" |> Array.map _.ToCharArray()

let mutable x, y = findStartingPosition lines '^'
let mutable visitedPositions = Set.empty.Add(x, y)
let mutable direction = Direction.Up

let mutable continueLooping = true
while (continueLooping) do
    visitedPositions <- visitedPositions.Add(x, y)
    printfn($"x: {x} y: {y}")

    let nextX, nextY =
        match direction with
        | Direction.Up -> x - 1, y
        | Direction.Right -> x, y + 1
        | Direction.Down -> x + 1, y
        | Direction.Left -> x, y - 1
        
    let nextSquare = safeAt lines nextX nextY

    if (nextSquare = '#') then
        direction <- Direction.getNextDirection direction
        ()
    else
        if(isOutsideOfBounds lines nextX nextY) then
            continueLooping <- false
            ()
        else 
            x <- nextX
            y <- nextY
            ()

printfn $"visitiedPositions: {visitedPositions.Count}"