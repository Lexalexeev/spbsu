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
 
let rec sort (arr : int []) (threadNum : int) (res : int [] ref) =
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
        l := sort arr.[0..(n / 2) - 1] (threadNum / 2) res
        ))
      rThread.Start()
      r := sort arr.[(n / 2)..(n - 1)] (threadNum / 2) res
      rThread.Join()
      lock res (fun _ -> mergeInRange l.Value r.Value res)
    else 
      mergeInRange (sort arr.[0..(n / 2) - 1] 0 res) (sort arr.[(n / 2)..(n - 1)] 0 res) res

let mergeSort (arr : int []) (threadNum : int) =
  let res = ref(Array.zeroCreate(arr.Length))
  sort arr threadNum res

[<EntryPoint>]
let main argv = 
  0