// Homework 9
// Alekseev Aleksei, group 171.

module pmax

open System.Threading

// Task 46 
let maxInRange (arr : int []) l r : int =
  let mutable res = arr.[0]
  for i in l .. r do
    if arr.[i] > res then
      res <- arr.[i]
  res

let max (arr : int []) (threadNum : int) =
  let arraySize = arr.Length
  let threadNum = min arraySize threadNum
  let res = ref 0
  let step = arraySize / threadNum   
  let threadArray = Array.init threadNum (fun i ->
    new Thread(ThreadStart(fun _ ->
        let threadRes = maxInRange arr (i * step) ((i + 1) * step - 1)
        lock res (fun _ -> res := max res.Value threadRes)
        ))
    )
  let threadRes = maxInRange arr ((threadNum - 1) * step) (arr.Length - 1)
  res := max res.Value threadRes
  for t in threadArray do
    t.Start()
  for t in threadArray do
    t.Join()
  res.Value

// Task 47 
let integralInRange (f : float -> float) l r e =
  let mutable res = 0.0
  for i in l .. e .. (r - e) do 
    res <- res + 0.5 * (f(i) + f(i + e)) * e
  res

let defIntegral (threadNumber : int) f l r e : float =
  let res = ref 0.0
  let step = (r - l) / (float threadNumber)
  let threadArray = Array.init threadNumber (fun i ->
      new Thread(ThreadStart(fun _ ->
          let threadRes =
            integralInRange f (l + step * (float i)) (l + step * (float (i + 1))) e
          lock res (fun _ -> res := res.Value + threadRes)
        ))
    )
  for t in threadArray do
    t.Start()
  for t in threadArray do
    t.Join()
  res.Value

let duration s f = 
  let timer = new System.Diagnostics.Stopwatch()
  timer.Start()
  let returnValue = f()
  printfn "Task: %s\t\t\tElapsed Time: %i" s timer.ElapsedMilliseconds
  returnValue

[<EntryPoint>]
let main argv =
  0