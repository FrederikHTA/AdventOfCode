module fsharp.Grid

open fsharp.Pos

module Grid =
    type Grid<'T when 'T : equality> = 'T[][]

    let create (data : 'T[][]) : Grid<'T> = data

    let get (grid : Grid<'T>) (pos : Pos) : 'T =
        grid.[pos.X].[pos.Y]

    let set (grid : Grid<'T>) (pos : Pos) (value : 'T) : unit =
        grid.[pos.X].[pos.Y] <- value

    let containsPos (pos : Pos) (grid : Grid<'T>) : bool =
        pos.X >= 0 && pos.X < Array.length grid && pos.Y >= 0 && pos.Y < Array.length grid.[0]

    let tryGet (pos : Pos) (defaultValue : 'T) (grid : Grid<'T>) : 'T =
        if grid |> containsPos pos then grid.[pos.X].[pos.Y] else defaultValue

    let transpose (grid : Grid<'T>) : Grid<'T> =
        let width = Array.length grid.[0]
        let height = Array.length grid
        Array.init width (fun i -> Array.init height (fun j -> grid.[j].[i]))

    let rotate (grid : Grid<'T>) : Grid<'T> =
        let width = Array.length grid.[0]
        let height = Array.length grid
        Array.init width (fun i -> Array.init height (fun j -> grid.[height - j - 1].[i]))

    let visualize (grid : Grid<'T>) =
        grid
        |> Array.iter (fun row ->
            row |> Array.iter (printf "%O")
            printfn ""
        )

        printfn "----------------------"

    let getPosOf (value : 'T) (grid : Grid<'T>) : Pos =
        grid
        |> Array.mapi (fun rowIndex row ->
            row
            |> Array.tryFindIndex ((=) value)
            |> Option.map (fun colIndex -> {
                X = rowIndex
                Y = colIndex
            })
        )
        |> Array.pick id // crashes if not found

    let width (grid : Grid<'T>) =
        if Array.isEmpty grid then 0 else Array.length grid.[0]

    let height (grid : Grid<'T>) =
        Array.length grid
