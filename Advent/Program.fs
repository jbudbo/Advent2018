// Learn more about F# at http://fsharp.org

open System

let private makePath file = sprintf "D:\AoC\Advent2018\%s.txt" file

let private dataLines file =
    System.IO.File.ReadAllLines (makePath file)

[<EntryPoint>]
let main argv =
    let day1Lines = dataLines "day1"

    day1Lines
    |> Array.map int
    |> Day1.part1
    |> printfn "Day Part 1 solution %i"

    // TODO
    //let solution = Day1.part2 "day1"

    let day2Lines = dataLines "day2"

    day2Lines |> Day2.part1 |> printfn "Day 2 Part 1 solution %i"

    day2Lines |> Day2.part2 |>  printfn "Day 2 Part 2 solution %s"

    let day3Lines = dataLines "day3"

    day3Lines |> Day3.Part1 |> printfn "Day 3 Part 1 solution %i"


    Console.Read() |> ignore

    0 // return an integer exit code
