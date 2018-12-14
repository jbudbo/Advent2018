﻿module Day6

open System.Text.RegularExpressions
    
let private parseLine l = 
    Regex.Match(l, "(\d{1,4}),\s(\d{1,4})") 
    |> fun m -> [for g in m.Groups -> g.Value] 
    |> fun g -> match g with [_;x;y] -> Some(int x, int y) | _ -> None

let part1 (lines:string[]) =
    //  Take our string data and make it into something we can work with (int*int)[]
    let anchorData = Array.choose parseLine lines

    let findOwner x y = 
        //  Let's first generate our values using the manhattan distance from our cell position
        [| for anchor in anchorData -> abs (x - fst anchor) + abs (y - snd anchor) |] 
        //  First check if we are sitting on an anchor, if so we don't need to process anymore
        |> fun heat ->
            Array.tryFindIndex (fun x -> x = 0) heat
            |> fun anchorIndex ->
                match anchorIndex with
                //  We did find a zero and it was X index
                | Some index -> Some(index)
                //  No such luck, time to do some work
                | None ->
                    heat
                    //  Let's group them all by how many times each ownership value occurs 1=sole ownership 2 or more is contested territory
                    |> Array.countBy id
                    //  Now lets get our smallest ownership and test if it's contested
                    |> Array.minBy fst
                    |> fun (value, owners) -> if owners = 1 then Some (heat |> Array.findIndex (fun x -> x = value)) else None

    //  Let's draw out the field so we can determine an owner any any given point
    let fullBoard = 
        //  Crack the anchor data so we can determine out tolerences (int[]*int[])
        let vertData = anchorData |> Array.unzip
        //  Find the minimum of each dimension given our anchors (min x * min y)
        let minima = vertData |> fun (xs,ys) -> ((Array.min xs),(Array.min ys))
        //  Find the maximum of each dimension given our anchors (max x * max y)
        let maxima = vertData |> fun (xs,ys) -> ((Array.max xs)-(fst minima),(Array.max ys)-(snd minima))
        //  Technically we are working with a 3 dimensional array however leaving this as a [int,int,int[]] saves us a step later on when we determine a winner for each cell
        //  Build up a constrained [,,[]] with an origin at our minima, a peak at our maxima, and cells containing ownership data correlating to each anchor
        //  [2,5,[1;2;3;4;5;6]]
        Array2D.initBased (fst minima) (snd minima) (fst maxima) (snd maxima) findOwner

    [| 
        for x in [fullBoard.GetLowerBound(0)..fullBoard.GetUpperBound(0)] do
        for y in [fullBoard.GetLowerBound(1)..fullBoard.GetUpperBound(1)] ->
        match fullBoard.[x,y] with Some x -> x | None -> -1
    |]
    |> Array.countBy id
    |> Array.sortByDescending snd
    //|> Array.maxBy snd
    //|> fun (winner,total) -> (winner, anchorData.[winner], total)

