module Day1

let private makePath file = sprintf "D:\AoC\Advent2018\%s.txt" file

let private dataLines file =
    System.IO.File.ReadAllLines (makePath file)
    |> Array.map int
    |> Array.toList

let part1 fileName = dataLines fileName |> List.sum

let part2 filename =
    let cache = (dataLines filename)

    let rec trans lst agg frequencies =
        match lst with
        | [] -> trans cache agg frequencies
        | h::t ->
            if List.contains agg frequencies then agg
            else
                trans t (agg + h) (agg::frequencies)

    trans cache 0 []