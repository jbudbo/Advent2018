module Day2

let private processString (chars:string) =
    chars.ToCharArray()
    |> Array.countBy id
    |> Array.where (fun (_,x) -> x > 1)
    |> Array.unzip
    |> snd
    |> Array.distinct

let private hamming (s1:string) (s2:string) = Seq.map2((=)) s1 s2
let private delta (s1:string) (s2:string) = hamming s1 s2 |> Seq.sumBy(fun b -> if b then 0 else 1)

let private getCandidates h t =
    t 
    |> List.map (fun box -> (h,box,(delta h box)))
    |> List.tryFind (fun (_,_,x) -> x = 1)

let part1 (lines: string[]) =
    let x = 
        lines
        |> Array.map (fun s -> processString s)
        |> Array.concat
        |> Array.countBy id
        |> Array.reduce (fun (_,x) (_,y) -> (x, y))
     
    (fst x) * (snd x)


let part2 (lines: string[]) =
    let rec compare (arr: string list) =
        match arr with
        | h::t -> 
            match getCandidates h t with
            | None -> compare t
            | Some (x,y,_) -> (x, hamming x y)
        | [] -> failwith "none found"
    
    let findings =
        lines
        |> Array.toList 
        |> compare

    let x =  findings
    
    let y =
        Seq.zip (fst x) (snd x)
        |> Seq.where (fun (_,y) -> y)
        |> Seq.map (fun (x,_) -> char x)
        |> Seq.toArray

    new string(y)