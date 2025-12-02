module fsharp._2025.Day2.Day2

open System.IO
open Xunit

let parseInput filePath =
    let lines = File.ReadAllText filePath

    lines.Split ','
    |> Seq.map (fun s ->
        let split = s.Split '-'
        (int split[0], int split[1])
    )
    |> Array.ofSeq

[<Fact>]
let ``part1`` () =
    let input = parseInput "2025/Day2/Data.txt"

    ()
// counter.Should().Be 1007

[<Fact>]
let ``part2`` () =
    let input = parseInput "2025/Day2/Data.txt"

    ()
// counter.Should().Be 1007
