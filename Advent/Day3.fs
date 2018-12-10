module Day3

open System.Text.RegularExpressions

let private (|Valid|_|) line =
    let m = Regex.Match (line, "#(\d+).@.(\d{1,4}),(\d{1,4}):.(\d{1,4})x(\d{1,4})")
    if m.Success then Some(List.tail [for g in m.Groups -> g.Value])
    else None

//let map2 mapping (array1: Array2D) (array2: Array2D) =
//    let res = Array2D.zeroCreate (Array2D.length1 array1) (Array2D.length2 array1)
//    for x = 


let private board = Array2D.create 1000 1000 0

let Part1 (lines:string[]) =
    let plot line =
        match line with
        | Valid [_; x; y; w; h] -> Array2D.createBased (int x) (int y) (int w) (int h) 1
        | _ -> failwith ""

    let combine (source: int[,]) sourceIndex1 sourceIndex2 (target: int[,]) targetIndex1 targetIndex2 count1 count2 =
        for i = 0 to count1 - 1 do
            for j = 0 to count2 - 1 do
                target.[targetIndex1+i, targetIndex2+j] <- target.[targetIndex1+i, targetIndex2+j] + source.[sourceIndex1+i,sourceIndex2+j]
    
    //let incIndex row index incVal =
    //    let parts = row |> List.splitAt index
    //    let incHead =
    //        match snd parts with
    //        | [] -> failwith "Fail"
    //        | [x] -> [x + incVal]
    //        | h::t -> (h + incVal)::t
            
    //    incHead |> List.append (fst parts)
        
    //let y = incIndex [1;2;3;4;5;] 2 3
        

    let plots = lines |> Array.map plot

    for plot in plots do
        combine plot (Array2D.base1 plot) (Array2D.base2 plot) board (Array2D.base1 plot) (Array2D.base2 plot) (Array2D.length1 plot) (Array2D.length2 plot)
    
    let mutable footage = 0

    for x = 0 to 999 do
        for y = 0 to 999 do
            if board.[x,y] >= 2 then footage <- footage + 1

    footage