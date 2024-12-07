module fsharp._2024.Day5.Day5

open System.IO
open Xunit
open Faqt
open fsharp.Extensions

let getPageOrderingRules (lines: Array<string>) =
    lines[0]
    |> fun x ->
        x.Split "\r\n"
        |> Array.map (fun x -> x.Split "|" |> fun y -> int y[0], int y[1])
        |> Array.groupBy fst
        |> Array.map (fun (k, v) -> (k, v |> Array.map snd))
        |> Map.ofArray

let getUpdates (lines: Array<string>) =
    lines[1]
    |> fun x -> x.Split "\r\n" |> Array.map (fun x -> x.Split "," |> Array.map int)

let isRowSorted (row: Array<int>) (pageOrderingRules: Map<int, Array<int>>) : bool =
    let mutable isSorted = true

    for i in 0 .. row.Length - 1 do
        if (i = 0) then
            ()
        else
            let currentEntry = row[i]
            let prevElements = row.[.. i - 1]
            let rules = pageOrderingRules.TryFind currentEntry |> Option.defaultValue [||]
            let isEqualLength = rules |> Array.except prevElements |> _.Length = rules.Length

            if not isEqualLength then
                isSorted <- false

    isSorted

let sortRow (row: Array<int>) (pageOrderingRules: Map<int, Array<int>>) : Array<int> =
    let mutable isSorted = false
    let mutable i = 0

    while isSorted = false do
        if (i = 0) then
            i <- i + 1
        else
            let currentEntry = row[i]
            let prevElements = row.[.. i - 1]
            let rules = pageOrderingRules.TryFind currentEntry |> Option.defaultValue [||]
            let isEqualLength = rules |> Array.except prevElements |> _.Length = rules.Length

            if not isEqualLength then
                let prev = row[i - 1]
                Array.set row (i - 1) currentEntry
                Array.set row i prev
                i <- i - 1
            else if i = row.Length - 1 then
                isSorted <- true
            else
                i <- i + 1

    row


let median (row: Array<int>) = row[row.Length / 2]

// MEGET grimt men det virker :)))))
// det bør kunne gøres drastisk simplere men idc
[<Fact>]
let ``part1`` () =
    let lines = File.ReadAllText "2024/Day5/Data.txt" |> fun x -> x.Split "\r\n\r\n"
    let pageOrderingRules = getPageOrderingRules lines
    let updates = getUpdates lines

    let sum =
        updates
        |> Array.filter (fun row -> isRowSorted row pageOrderingRules)
        |> Array.sumBy median

    sum.Should().Be(4185)

[<Fact>]
let ``part2`` () =
    let lines = File.ReadAllText "2024/Day5/Data.txt" |> fun x -> x.Split "\r\n\r\n"

    let pageOrderingRules = getPageOrderingRules lines
    let updates = getUpdates lines

    let sum =
        updates
        |> Array.filter (fun row -> isRowSorted row pageOrderingRules |> not)
        |> Array.map (fun row -> sortRow row pageOrderingRules )
        |> Array.sumBy median

    sum.Should().Be(4480)
