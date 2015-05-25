// Homework 10
// Alekseev Aleksei, group 171.

module matrix

open System.Threading

let calcInRange (A : int[,]) (B : int[,]) (res : int[,]) (columnB : int) (columnA : int) (l : int) (r : int) =
  let mutable temp = 0
  for k = l to r do
    for i = 0 to columnB - 1 do
      temp <- 0
      for j = 0 to columnA - 1 do
        temp <- temp + (A.[k,j] * B.[j,i])
      res.[k,i] <- temp

let calc (A : int[,]) (B : int[,]) (threadNum : int) =
  let lineA = A.GetLength 0
  let columnA = A.GetLength 1
  let lineB = B.GetLength 0
  let columnB = B.GetLength 1
  let res = Array2D.zeroCreate lineA columnB
  let step = lineA / threadNum   
  let threadArray = Array.init threadNum (fun i ->
    new Thread(ThreadStart(fun _ ->
        calcInRange A B res columnB columnA (i * step) ((i + 1) * step - 1)
        ))
    )
  for t in threadArray do
    t.Start()
  for t in threadArray do
    t.Join()
  if (step * threadNum) < lineA then
    calcInRange A B res columnB columnA (threadNum * step) (lineA - 1)
  res

let duration s f = 
  let timer = new System.Diagnostics.Stopwatch()
  timer.Start()
  let returnValue = f()
  printfn "Task: %s\t\t\tElapsed Time: %i" s timer.ElapsedMilliseconds
  returnValue

[<EntryPoint>]
let main argv =
  0
