open System.IO

// Day 2: Gift Shop.
let parseRanges fileName =
    (File.ReadAllText fileName).Split(",")
    |> Array.map (fun r ->
        let range = r.Split("-")
        (int64 range.[0], int64 range.[1]))

let isInvalid1 (n: int64) =
    let nStr = n.ToString()
    let nStrLen = nStr.Length
    let nStrMidPoint = nStrLen/2
    if nStrLen % 2 = 1 then
        false
    else
        let lhs = nStr.[0..nStrMidPoint-1]
        let rhs = nStr.[nStrMidPoint..nStrLen]
        lhs = rhs

let isInvalid2 (n: int64) =
    let nStr = n.ToString()
    let nStrLen = nStr.Length
    let nStrMidPoint = int nStrLen/2

    let isInvalidChunks chunks =
        chunks
        |> Array.map (fun c -> c = chunks.[0])
        |> Array.reduce (&&)

    let noInvalidSplits =
        [|1..nStrMidPoint|]
        |> Array.map (fun s -> nStr |> Seq.chunkBySize s |> Array.ofSeq)
        |> Array.filter (fun s -> isInvalidChunks s)
        |> Array.length                    
    noInvalidSplits > 0

[<EntryPoint>]
let main argv =
    let ranges = parseRanges "Day02.txt"        

    let getInvalidIds range isInValidFn =
        [|fst range..snd range|]
        |> Array.filter (fun r -> isInValidFn r)

    let invalidIds1 =
        ranges
        |> Array.map (fun r -> getInvalidIds r isInvalid1)
        |> Array.collect (fun r -> r)

    let invalidIds2 =
        ranges
        |> Array.map (fun r -> getInvalidIds r isInvalid2)
        |> Array.collect (fun r -> r)
    
    printfn "Part 1 answer: %d" (Array.sum invalidIds1)
    printfn "Part 2 answer: %d" (Array.sum invalidIds2)
    0