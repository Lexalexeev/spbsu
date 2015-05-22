// Homework 10
// Alekseev Aleksei, group 171.

module mergesort

open System.Threading

let rec mergeInRange (l : int []) (r : int []) (res : int [] ref) =
  let n = l.Length + r.Length
  let mutable i = 0
  let mutable j = 0
  for k = 0 to n - 1 do
    if i >= l.Length   then 
      res.Value.[k] <- r.[j]
      j <- j + 1
    elif j >= r.Length then 
      res.Value.[k] <- l.[i]
      i <- i + 1
    elif l.[i] < r.[j] then 
      res.Value.[k] <- l.[i]
      i <- i + 1
    else 
      res.Value.[k] <- r.[j]
      j <- j + 1
  res.Value
 
let rec mergeSort (arr : int []) (threadNum : int) =
  match arr with
  | [||] -> [||]
  | [|a|] -> [|a|]
  | arr ->   
    let res = ref (Array.zeroCreate(arr.Length))
    let n = arr.Length
    if threadNum > 1 then
      let l = ref [||]
      let r = ref [||]
      let rThread = new Thread(ThreadStart(fun _ ->
        l := mergeSort arr.[0..(n / 2) - 1] (threadNum / 2)
        ))
      rThread.Start()
      r := mergeSort arr.[(n / 2)..(n - 1)] (threadNum / 2)
      rThread.Join()
      lock res (fun _ -> mergeInRange l.Value r.Value res)
    else 
      mergeInRange (mergeSort arr.[0..(n / 2) - 1] 0) (mergeSort arr.[(n / 2)..(n - 1)] 0) res

let duration s f = 
  let timer = new System.Diagnostics.Stopwatch()
  timer.Start()
  let returnValue = f()
  printfn "Task: %s\t\t\tElapsed Time: %i" s timer.ElapsedMilliseconds
  returnValue

[<EntryPoint>]
let main argv = 
  0