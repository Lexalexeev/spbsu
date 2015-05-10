// Homework 7
// Alekseev Aleksei, group 171.

module Workflow

// Task 40
type RingBuilder(n : int) =
  member this.Bind (x, f) = f (x % n)
  member this.Return x =
    if (x % n < 0) then (x % n + n) else (x % n)
let ring n = RingBuilder n

[<EntryPoint>]
let main argv =
  0