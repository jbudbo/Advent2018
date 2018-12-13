module Day1

let part1 (lines: int[]) = lines |> Array.sum

let part2 (lines: int[]) =
    let isDupe (x,y) = if y > 1 then Some(x) else None

    //let rec walkData (data: int[]) (init:int)
    //    let totalSum = part1 data
    //    match data |> Array.scan (+) init |> Array.countBy id |> Array.choose isDupe with
    //    | [||] -> walkData data totalSum
    //    | [|x|] -> x
    //    | _ -> failwith ""
        
    //let x = walkData lines 0

    0
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