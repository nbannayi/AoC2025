open System.IO
open System

// Day 8: Playground.
let getDistance (coord1: (int * int * int)) (coord2: (int * int * int)) =
    let (x1,y1,z1) = coord1
    let (x2,y2,z2) = coord2 
    Math.Sqrt((double (x2-x1)) ** 2. + (double (y2-y1)) ** 2. + (double (z2-z1)) ** 2.)

let getShortestCombos (coords : (int * int * int) array) n =
    let combos =
        [|for i in 0..coords.Length-1 do
            for j in i+1..coords.Length-1 ->
                getDistance coords.[i] coords.[j], (coords.[i], coords.[j])|]
        |> Array.sortBy (fun (d,_) -> d)
    if n > 0 then
        combos |> Array.take(n) 
    else
        combos
    |> Array.map (fun (_,coords) -> coords)

let partitionCoords (combos: ((int * int * int) * (int * int * int)) array) =
    let partitions = ResizeArray<(int * int * int) list>()    
    combos
    |> Array.iter (fun (c1,c2) ->
        let candidateIndex1 = 
            partitions
            |> Seq.tryFindIndex (fun p -> p |> List.contains c1)
        let candidateIndex2 = 
            partitions
            |> Seq.tryFindIndex (fun p -> p |> List.contains c2)
        match candidateIndex1, candidateIndex2 with
        | Some i, None -> partitions.[i] <- partitions.[i] @ [c1;c2]
        | None, Some i -> partitions.[i] <- partitions.[i] @ [c1;c2]
        | Some i, Some j ->
            if i <> j then
                partitions.[i] <- partitions.[i] @ partitions.[j] @ [c1;c2]
                partitions.RemoveAt(j)
        | _ -> partitions.Add([c1;c2]))
    partitions
    |> Seq.map (fun l -> l |> List.distinct)
    |> Array.ofSeq

let getClosedCircuitSize (combos: ((int * int * int) * (int * int * int)) array) =
    let partitions =
        combos
        |> partitionCoords    
    partitions.[0] |> List.length
    
[<EntryPoint>]
let main argv =    
    let noPairs = 1000 // Change for example vs. actual data.    
    let closedCircuitSize = 1000

    let coords =
        File.ReadAllLines "Day08.txt"
        |> Array.map (fun l ->
            let coords = l.Split(",")
            (int coords.[0], int coords.[1], int coords.[2]))

    let product3LargestCircuits =
        getShortestCombos coords noPairs
        |> partitionCoords
        |> Array.map (fun p -> p.Length)
        |> Array.sortDescending
        |> Array.take(3)
        |> Array.reduce (*)    

    printfn $"Part 1 answer: {product3LargestCircuits}"
    
    let rec findClosedCircuit i =
        let combos = getShortestCombos coords i
        let size = getClosedCircuitSize combos
        if size = closedCircuitSize then            
            i-1
        else
            findClosedCircuit (i+1)

    // Used trial and error - need to implement binary chop at some point.
    let closedCircuitIndex = findClosedCircuit 6384 
    let combo = (getShortestCombos coords 0).[closedCircuitIndex]
    let extensionLength =
        let (c1,c2) = combo
        let (x1,_,_) = c1
        let (x2,_,_) = c2
        int64 x1 * int64 x2

    printfn $"Part 2 answer: {extensionLength}"
    0