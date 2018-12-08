module Day1

let part1 (lines: int[]) = lines |> Array.sum

//let part2 filename =
//    let cache = (dataLines filename)

//    let rec trans lst agg frequencies =
//        match lst with
//        | [] -> trans cache agg frequencies
//        | h::t ->
//            if List.contains agg frequencies then agg
//            else
//                trans t (agg + h) (agg::frequencies)

//    trans cache 0 []