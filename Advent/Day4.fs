module Day4

open System
open System.Text.RegularExpressions


type Event =
    | OnDuty of DateTime * int
    | SleepStart of Minute : DateTime
    | Awoke of Minute : DateTime




let private (|Parse|_|) line =
    let m = Regex.Match (line, "\[(\d{4}-\d{2}-\d{2}.\d{2}:\d{2})\].(.+)")
    if m.Success then Some(List.tail [for g in m.Groups -> g.Value])
    else None

let part1 (lines:string[]) =
    let parseEntry line =
        match line with
        | Parse [d; t] -> (DateTime.Parse d, t)
        | _ -> failwith ""

    let y =
        lines
        |> Array.map parseEntry
        |> Array.sortBy fst

    

    0
    //String.