module fsharp.Extensions

open System

[<AutoOpen>]
type Array<'T> = Microsoft.FSharp.Core.array<'T>

module Array =
    let median (row: Array<int>) = row[row.Length / 2]
    
let toTuple<'T> (sep: char) (convert: string -> 'T) (input: string) : 'T * 'T =
    match input.Split(sep, StringSplitOptions.RemoveEmptyEntries) with
    | [|a; b|] -> (convert a, convert b)
    | _ -> failwith $"Invalid tuple format: {input}"
    
let convertTuple<'T, 'U> (convert: 'T -> 'U) (input: 'T * 'T) : 'U * 'U =
    let a, b = input
    (convert a, convert b)
