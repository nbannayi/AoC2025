open System.IO

// Day 6: Trash Compactor.
let parseSums1 inputFile = 
    File.ReadAllLines inputFile    
    |> Array.map (fun i ->
        i.Split(' ') |> Array.filter (fun j -> j.Trim() <> ""))
    |> Array.transpose

let getGrandTotal inputSums =
    inputSums
    |> Array.map (fun i ->            
        let operands = i |> Array.take(4) |> Array.map (int64)
        let operator = i.[4]
        match operator with
        | "+" -> operands |> Array.reduce (+)
        | _   -> operands |> Array.reduce (*))
    |> Array.sum

[<EntryPoint>]
let main argv =
    let fileName = "Day06.txt"

    let inputSums1 = parseSums1 fileName    
    printfn $"Part 1 answer: {getGrandTotal inputSums1}"
    0