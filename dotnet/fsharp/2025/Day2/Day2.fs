module fsharp._2025.Day2.Day2

open System.IO
open Expecto
open Xunit

type IdRange = {
    FirstId : double
    LastId : double
}

let parseInput filePath =
    let lines = File.ReadAllText filePath

    lines.Split ','
    |> Seq.map (fun s ->
        let split = s.Split '-'

        {
            FirstId = double split[0]
            LastId = double split[1]
        }
    )
    |> Array.ofSeq

// all of this could propably be done with regex but whatever
[<Fact>]
let ``part1`` () =
    let input = parseInput "2025/Day2/Data.txt"

    input
    |> Array.collect (fun idRange ->
        [| idRange.FirstId .. idRange.LastId |]
        |> Array.map _.ToString()
        |> Array.filter (fun s -> s.Length % 2 = 0)
        |> Array.filter (fun s ->
            let midpoint = s.Length / 2
            let firstHalf, secondHalf = s[.. midpoint - 1], s[midpoint..]
            firstHalf = secondHalf
        )
    )
    |> Array.map double
    |> Array.sum
    |> Flip.Expect.equal "equal" 15873079081.0

[<Fact>]
let ``part2`` () =
    let input = parseInput "2025/Day2/Data.txt"

    input
    |> Array.collect (fun idRange ->
        [| idRange.FirstId .. idRange.LastId |]
        |> Array.map _.ToString()
        |> Array.filter (fun s ->
            let len = s.Length
            let divisors = [ 2..len ] |> List.filter (fun i -> len % i = 0)

            divisors
            |> List.exists (fun n ->
                let distinct =
                    s.ToCharArray ()
                    |> Array.chunkBySize (len / n)
                    |> Array.map System.String
                    |> Array.distinct

                distinct.Length = 1
            )
        )
    )
    |> Array.map double
    |> Array.sum
    |> Flip.Expect.equal "equal" 22617871034.0
