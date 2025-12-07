open System.IO

// Day 6: Trash Compactor.
let parseSums1 inputFile = 
    File.ReadAllLines inputFile    
    |> Array.map (fun i -> i.Split(' ') |> Array.filter (fun j -> j.Trim() <> ""))
    |> Array.transpose

let parseSums2 inputFile =
    let lines = File.ReadAllLines inputFile
    let operands = 
        let line = 
            [|0..(String.length lines.[0])-1|]
            |> Array.map (fun i -> string lines.[0].[i] + string lines.[1].[i] + string lines.[2].[i] + string lines.[3].[i])
            |> String.concat " "
        line.Split(" " |> String.replicate 6)
        |> Array.map (fun n -> n.Trim().Split(" ") |> Array.filter (fun n -> n <> ""))
    let operators = lines.[4].Split(" ") |> Array.filter (fun o -> o <> "")
    Array.zip operands operators |> Array.map (fun (a,b) -> Array.append a [|b|])

let getGrandTotal inputSums =
    inputSums
    |> Array.map (fun i ->            
        let length = i |> Array.length
        let operands = i |> Array.take(length-1) |> Array.map (int64)
        let operator = i.[length-1]
        match operator with
        | "+" -> operands |> Array.reduce (+)
        | _   -> operands |> Array.reduce (*))
    |> Array.sum

[<EntryPoint>]
let main argv =
    let fileName = "Day06.txt"
    let inputSums1 = parseSums1 fileName
    let inputSums2 = parseSums2 fileName
    printfn $"Part 1 answer: {getGrandTotal inputSums1}"
    printfn $"Part 2 answer: {getGrandTotal inputSums2}"
    0