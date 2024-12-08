module fsharp.Pos

type Pos = { X: int; Y: int }

module Pos =
    // Manhattan Distance
    let manhattanDistance (p1: Pos) (p2: Pos) : int = abs (p1.X - p2.X) + abs (p1.Y - p2.Y)

    // Operators for adding, subtracting, and multiplying positions
    let (+) (p1: Pos) (p2: Pos) : Pos = { X = p1.X + p2.X; Y = p1.Y + p2.Y }
    let (-) (p1: Pos) (p2: Pos) : Pos = { X = p1.X - p2.X; Y = p1.Y - p2.Y }
    let (*) (p1: Pos) (p2: Pos) : Pos = { X = p1.X * p2.X; Y = p1.Y * p2.Y }

    // Comparison operators for <= and >=
    let (<=) (p1: Pos) (p2: Pos) : bool = p1.X <= p2.X && p1.Y <= p2.Y
    let (>=) (p1: Pos) (p2: Pos) : bool = p1.X >= p2.X && p1.Y >= p2.Y

    // Axis Offsets
    let axisOffsets: List<Pos> =
        [ { X = -1; Y = 0 }; { X = 0; Y = -1 }; { X = 0; Y = 1 }; { X = 1; Y = 0 } ]

    // Diagonal Offsets
    let diagonalOffsets: List<Pos> =
        [ { X = -1; Y = 1 }; { X = 1; Y = 1 }; { X = -1; Y = -1 }; { X = 1; Y = -1 } ]

    // Get Axis Offsets
    let getAxisOffsets (pos: Pos) : List<Pos> =
        axisOffsets |> List.map (fun offset -> pos + offset)

    // Get Diagonal Offsets
    let getDiagonalOffsets (pos: Pos) : List<Pos> =
        diagonalOffsets |> List.map (fun offset -> pos + offset)

    // Get All Offsets
    let getAllOffsets (pos: Pos) : List<Pos> =
        List.append (getAxisOffsets pos) (getDiagonalOffsets pos)

    // Zero Position
    let zero: Pos = { X = 0; Y = 0 }
