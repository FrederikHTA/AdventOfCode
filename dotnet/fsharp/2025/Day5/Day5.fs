module fsharp._2025.Day5.Day5

open System.IO
open System.Numerics
open Xunit
open Expecto

type IdRange = {
    FirstId : bigint
    LastId : bigint
}

let parseIdRange (s : string) =
    let lines = s.Split '\n'

    lines
    |> Array.map (fun s ->
        let split = s.Split '-'

        {
            FirstId = bigint.Parse split[0]
            LastId = bigint.Parse split[1]
        }
    )

let parseIds (s : string) =
    s.Trim().Split "\n" |> Array.map bigint.Parse

let parseInput filePath =
    let lines = File.ReadAllText filePath
    let split = lines.Trim().Split "\n\n"

    match split with
    | [| ranges ; ids |] -> parseIdRange ranges, parseIds ids
    | _ -> failwith "Invalid input"

// all of this could propably be done with regex but whatever
[<Fact>]
let ``part1`` () =
    let idRanges, ids = parseInput "2025/Day5/Data.txt"

    ids
    |> Array.filter (fun id ->
        idRanges |> Array.exists (fun range -> id >= range.FirstId && id <= range.LastId)
    )
    |> Array.length
    |> Flip.Expect.equal "equal" 505

let mergeRanges (ranges : IdRange[]) =
    ranges
    |> Array.sortBy _.FirstId
    |> Array.fold
        (fun acc range ->
            match acc with
            | [] -> [ range ]
            | _ ->
                let last = List.head acc

                if last.LastId >= range.FirstId then
                    let mergedRange = {
                        FirstId = last.FirstId
                        LastId = max last.LastId range.LastId
                    }

                    let tail = acc |> List.tail
                    mergedRange :: tail
                else
                    range :: acc
        )
        []
    |> Array.ofList

[<Fact>]
let ``part2`` () =
    let idRanges, _ = parseInput "2025/Day5/Data.txt"

    idRanges
    |> mergeRanges
    |> Array.sumBy (fun range -> (range.LastId - range.FirstId) + bigint.One)
    |> Flip.Expect.equal "equal" 344423158480189I
