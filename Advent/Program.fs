 // Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main argv =
    printfn "Day 1 solution %i" (Day1.part1 "day1")

    let solution =
        Day1.part2 "day1"

    Console.Read() |> ignore

    0 // return an integer exit code
