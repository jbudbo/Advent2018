module Day3

open System.Text.RegularExpressions

let private (|Valid|_|) line =
    let m = Regex.Match (line, "#(\d+).@.(\d{1,4}),(\d{1,4}):.(\d{1,4})x(\d{1,4})")
    if m.Success then Some(List.tail [for g in m.Groups -> g.Value])
    else None

let private board = Array2D.create 1000 1000 0

let Part1 (lines:string[]) =
    let plot line =
        match line with
        | Valid [_; x; y; w; h] -> Array2D.createBased (int x) (int y) (int w) (int h) 1
        | _ -> failwith ""

    
    let incIndex row index incVal =
        let parts = row |> List.splitAt index
        let incHead =
            match snd parts with
            | [] -> failwith "Fail"
            | [x] -> [x + incVal]
            | h::t -> (h + incVal)::t
            
        incHead |> List.append (fst parts)
        

    
    let y = incIndex [1;2;3;4;5;] 2 3
        

    let x =
        lines
        |> Array.map plot

    
    

    0