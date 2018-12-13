module Day5

let private condense (polymer:char list) :char list =
    let collapse (segment:char list) :char list option=
        match segment |> List.map int |> List.reduce (-) |> abs with
        | 32 -> None
        | _ -> Some(segment)

    let react (polymer:char list) :char list = polymer |> List.chunkBySize 2 |> List.choose collapse |> List.concat

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

    playReactions polymer

let part1 line = line |> Seq.toList |> condense |> List.length    
    

let part2 line =
    let removals = List.zip ['a'..'z'] ['A'..'Z']

    let updatePolymers (polymer : char list) = seq {
        for (p1,p2) in removals do
            yield
                polymer
                //|> List.except [p1;p2] // NOTE: Swapping this out because it seemed to max out at 50 results returned no matter what
                |> List.filter (fun c -> not (c = p1 || c = p2))
                |> condense
    }

    line |> Seq.toList |> updatePolymers |> Seq.map (fun s -> Seq.length s) |> Seq.min
