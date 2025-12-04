open System.IO

// Day 4: Printing Department.
let canAccessRoll row col grid =
    [|(row+1, col);   (row-1, col);   (row, col-1);   (row, col+1);
        (row+1, col+1); (row+1, col-1); (row-1, col-1); (row-1,col+1)|]
    |> Array.filter (fun (r,c) ->
        r >= 0 && r < (Array2D.length1 grid) &&
        c >= 0 && c < (Array2D.length2 grid))
    |> Array.map (fun (r,c) -> if grid.[r,c] = '@' then 1 else 0)
    |> Array.sum
    |> (>) 4

let isRoll row col grid =        
    row >= 0 && row < (Array2D.length1 grid) &&
    col >= 0 && col < (Array2D.length2 grid) &&
    grid.[row,col] = '@'

let parseGrid fileName =
    let grid' = File.ReadAllLines fileName
    let maxRow = Array.length grid'
    let maxCol = grid'.[0].Length
    let grid = Array2D.init maxRow maxCol (fun _ _ -> '.')
    [for row in 0..maxRow-1 do
        for col in 0..maxCol-1 ->
            grid.[row,col] <- grid'.[row].[col]]
    |> ignore
    grid

let getAccessibleRolls grid =    
    [for row in 0..(Array2D.length1 grid)-1 do
        for col in 0..(Array2D.length2 grid)-1 -> (row,col)]
    |> List.filter (fun (r,c) -> grid |> isRoll r c && grid |> canAccessRoll r c)        

let removeAccessibleRolls (rolls: (int * int) list) (grid: char [,]) =
    [for (row,col) in rolls -> grid.[row,col] <- '.']
    |> ignore
    grid

let rec countAllAccessibleRolls count grid =
    let accessibleRolls = grid |> getAccessibleRolls
    if accessibleRolls.Length = 0 then
        count
    else
        grid
        |> removeAccessibleRolls accessibleRolls
        |> countAllAccessibleRolls (count+accessibleRolls.Length)

[<EntryPoint>]
let main argv =
    let grid = parseGrid "Day04.txt"    
    printfn "Part 1 answer: %d" (grid |> getAccessibleRolls).Length
    printfn "Part 2 answer: %d" (grid |> countAllAccessibleRolls 0)    
    0