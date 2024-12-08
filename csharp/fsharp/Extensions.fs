module fsharp.Extensions

open System

[<AutoOpen>]
type Array<'T> = Microsoft.FSharp.Core.array<'T>

let convertTuple<'T, 'U> (convert: 'T -> 'U) (input: 'T * 'T) : 'U * 'U =
    let a, b = input
    (convert a, convert b)
    
module Array =
    let median (row: Array<'T>) = row[row.Length / 2]
    let allPairs (arr: Array<'T>) = 
        [| for i in 0 .. arr.Length - 1 do
               for j in i + 1 .. arr.Length - 1 do
                   yield (arr[i], arr[j]) |]
        
module String =
    let toTuple<'T> (sep: char) (convert: string -> 'T) (input: string) : 'T * 'T =
        match input.Split(sep, StringSplitOptions.RemoveEmptyEntries) with
        | [|a; b|] -> (convert a, convert b)
        | _ -> failwith $"Invalid tuple format: {input}"