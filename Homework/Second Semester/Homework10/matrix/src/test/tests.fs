// Homework 10
// Alekseev Aleksei, group 171.

module tests

open NUnit.Framework
open matrix

[<Test>]
let ``TestMatrix`` () =
  let A = array2D [| [| 1; 3; 2; 1|] ; [| 2; 0; 1; 2|] ; [| 5;-2; 3; 4|] ; [| 6; 1; 5; 1|] ; [| 4; 2;-3; 0|] ; [|-1; 2; 3;-5|] |]
  let B = array2D [| [|1;1;0;5;2|] ; [|-2;-3;2;1;0|] ; [|0;1;0;0;-4|] ; [|2;4;6;2;1|] |]
  let res = array2D [| [|-3;-2;12;10;-5|] ; [|6;11;12;14;2|] ; [|17;30;20;31;2|] ; [|6;12;8;33;-7|] ; [|0;-5;4;22;20|] ; [|-15;-24;-26;-13;-19|] |]
  for i = 1 to 4 do
    Assert.AreEqual(res, calc A B i) 

(*
---Matrix 1000x800 * Matrix 800x600---
treadNumber: 1         Elapsed Time: 14126
treadNumber: 2         Elapsed Time: 9944
treadNumber: 3         Elapsed Time: 7551
treadNumber: 4         Elapsed Time: 6157
*)