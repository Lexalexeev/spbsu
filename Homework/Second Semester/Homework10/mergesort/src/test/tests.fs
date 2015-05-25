// Homework 10
// Alekseev Aleksei, group 171.

module tests

open NUnit.Framework
open mergesort

[<Test>]
let ``TestMergesort`` () =
  let arr = [|9;0;-3;10;4;7;2;-2;8;6;5;3;1;-1|]
  let res = [|-3;-2;-1;0;1;2;3;4;5;6;7;8;9;10|]
  for i = 1 to 4 do
    Assert.AreEqual(res, mergeSort arr i) 

(*
---Array 10^7---
treadNumber: 1         Elapsed Time: 6628
treadNumber: 2         Elapsed Time: 5430
treadNumber: 3         Elapsed Time: 5790
treadNumber: 4         Elapsed Time: 4718
*)