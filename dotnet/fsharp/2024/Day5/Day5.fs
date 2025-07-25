﻿module fsharp._2024.Day5.Day5

open System.IO
open Xunit
open Faqt
open fsharp

let getPageOrderingRules (lines : Array<string>) =
    lines[0] |> fun x -> x.Split "\r\n" |> Array.map (fun x -> x |> String.toTuple '|' int)

let getUpdates (lines : Array<string>) =
    lines[1] |> fun x -> x.Split "\r\n" |> Array.map (fun x -> x.Split "," |> Array.map int)

let sortRow (row : Array<int>) (pageOrderingRules : Map<int, Array<int>>) : Array<int> =
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

// refactored solution inspired by https://github.com/exynoxx
[<Fact>]
let ``part1`` () =
    let lines = File.ReadAllText "2024/Day5/Data.txt" |> fun x -> x.Split "\r\n\r\n"
    let pageOrderingRules = getPageOrderingRules lines
    let updates = getUpdates lines

    let inverseLookup = pageOrderingRules |> Seq.map (fun (a, b) -> (b, a)) |> Set

    let isOrdered (updates : int array) =
        updates |> Array.allPairs |> Array.exists inverseLookup.Contains |> not

    let result = updates |> Seq.filter isOrdered |> Seq.sumBy Array.median

    result.Should().Be (4185)

// sortWith doesnt work and i dont know why, so i do it manually....
[<Fact>]
let ``part2`` () =
    let lines = File.ReadAllText "2024/Day5/Data.txt" |> fun x -> x.Split "\r\n\r\n"
    let pageOrderingRules = getPageOrderingRules lines
    let updates = getUpdates lines

    let inverseLookup = pageOrderingRules |> Seq.map (fun (a, b) -> (b, a)) |> Set

    let isOrdered (updates : int array) =
        updates |> Array.allPairs |> Array.exists inverseLookup.Contains |> not

    let sum =
        updates
        |> Array.filter (fun x -> x |> isOrdered |> not)
        |> Array.map (fun row ->
            let pageOrderingMap =
                pageOrderingRules |> Array.groupBy fst |> Array.map (fun (k, v) -> (k, v |> Array.map snd)) |> Map.ofArray

            sortRow row pageOrderingMap
        )
        |> Array.sumBy Array.median

    sum.Should().Be (4480)
