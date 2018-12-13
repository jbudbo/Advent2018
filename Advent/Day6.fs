module Day6

open System.Text.RegularExpressions

let private parseLine l = 
    Regex.Match(l, "(\d{1,4}),\s(\d{1,4})") 
    |> fun m -> List.tail [for g in m.Groups -> g.Value] 
    |> fun g -> match g with [x;y] -> (int x, int y) | _ -> (0,0)

let part1 (lines:string[]) =
    let numOfAnchors = lines.Length
    let anchors = lines |> Array.map parseLine 

    let getAnchorOwnership x1 y1 anchorIndex = anchors.[anchorIndex] |> fun (x2,y2) -> (abs (x1 - x2)) + (abs (y1 - y2))

    let findOwner x y (opData:int[,,]) = 
        match Array.tryFindIndex (fun (a,b) -> a = x && b = y) anchors with
        //  If we're on a location that is explicetly held by one of our anchors, don't bother just pass back our anchor index
        | Some anchorIndex -> anchorIndex
        //  Otherwise we have to calculate who owns this
        | None ->
            //  Turn the entirety of our Z data into an array at this position so we can work with it
            let distances = seq { for i in [0..numOfAnchors-1] do yield opData.[x,y,i] } |> Seq.toArray
            //  Group it by frequency, 2 or above means it's contested by multiple anchors
            distances
                |> Array.countBy id 
                |> Array.partition (fun owners -> snd owners = 1)
                |> fun (uncontested,_) -> uncontested |> Array.map fst |> Array.min 
                |> fun closest -> distances |> Array.tryFindIndex (fun distance -> distance = closest)
                |> fun owner -> match owner with Some anchor -> anchor | None -> failwith "Could not determine an owner for this cell"

    let winner =
        anchors
        |> Array.unzip
        |> fun (data:int[]*int[]) -> 
            data
            |> fun (xs, ys) -> (Array.max xs, Array.max ys)
            |> fun (maxX, maxY) -> Array3D.init maxY maxX numOfAnchors getAnchorOwnership
        |> fun board3d -> Array2D.init (Array3D.length1 board3d) (Array3D.length2 board3d) (fun x y -> findOwner x y board3d)
        |> fun ownerData ->
            let maxDimensions = (Array2D.length1 ownerData - 1, Array2D.length2 ownerData - 1)
            [|0..fst maxDimensions|] 
            |> Array.mapi (fun index _ -> ownerData.[index, 0..snd maxDimensions] |> Array.countBy id |> Array.maxBy snd)
        |> fun eligableAnchors ->
            [| for i in eligableAnchors |> Array.map fst |> Array.distinct -> 
                i,eligableAnchors |> Array.where (fun elem -> fst elem = i ) |> Array.sumBy snd |]
        |> Array.maxBy snd
        //|> fst
        //|> fun winner -> anchors.[winner]
            
    snd winner
