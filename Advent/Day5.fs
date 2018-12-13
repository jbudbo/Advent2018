module Day5


let part1 line =
    let collapse (segment:char list) :char list =
        match segment |> List.map int |> List.reduce (-) |> abs with
        | 32 -> []
        | _ -> segment

    let react (polymer:char list) :char list = polymer |> List.chunkBySize 2 |> List.map collapse |> List.concat

    let reactOffset (polymer:char list) :char list =
        match polymer with
        | h::t -> h::(react t)
        | _ -> polymer

    let rec playReactions (polymer: char list) :char list =
        let result = polymer |> react |> reactOffset
        if result.Length = polymer.Length then 
            result
        else
            playReactions result

    line |> Seq.toList |> playReactions |> List.length    
    