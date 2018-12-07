module Day2

let private makePath file = sprintf "D:\AoC\Advent2018\%s.txt" file

let private dataLines file =
    System.IO.File.ReadAllLines (makePath file)

let private processString (chars:string) =
    chars.ToCharArray()
    |> Array.countBy id
    |> Array.where (fun (_,x) -> x > 1)
    |> Array.unzip
    |> snd
    |> Array.distinct

let part1 fileName =
    let x = 
        dataLines fileName
        |> Array.map (fun s -> processString s)
        |> Array.concat
        |> Array.countBy id
        |> Array.reduce (fun (_,x) (_,y) -> (x, y))
     
    (fst x) * (snd x)
