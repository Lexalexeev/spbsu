// Homework 9
// Alekseev Aleksei, group 171.

module tests

open NUnit.Framework
open pmax

[<TestCase (1, Result = 999999)>]
[<TestCase (2, Result = 999999)>]
[<TestCase (3, Result = 999999)>]
[<TestCase (4, Result = 999999)>]
let ``TestMaxRnd6`` (t : int) =
  let degree = 6
  let n = pown 10 degree
  let rnd = new System.Random(0)
  let arr = Array.init n (fun i -> rnd.Next(0 , n))
  max arr t

[<TestCase (1, Result = 99999998)>]
[<TestCase (2, Result = 99999998)>]
[<TestCase (3, Result = 99999998)>]
[<TestCase (4, Result = 99999998)>]
let ``TestMaxRnd8`` (t : int) =
  let degree = 8
  let n = pown 10 degree
  let rnd = new System.Random(0)
  let arr = Array.init n (fun i -> rnd.Next(0 , n))
  max arr t

[<TestCase ([|1;7;3;0;4;5;8;6;2;1;7;3;0;4;5;8;6;2;9;1;7;3;0;4;5;8;6;2|],1, Result = 9)>]
[<TestCase ([|1;7;3;0;4;5;8;6;2;1;7;3;0;4;5;8;6;2;9;1;7;3;0;4;5;8;6;2|],2, Result = 9)>]
[<TestCase ([|1;7;3;0;4;5;8;6;2;1;7;3;0;4;5;8;6;2;9;1;7;3;0;4;5;8;6;2|],3, Result = 9)>]
[<TestCase ([|1;7;3;0;4;5;8;6;2;1;7;3;0;4;5;8;6;2;9;1;7;3;0;4;5;8;6;2|],4, Result = 9)>]
let ``TestMax`` (arr : int []) (t : int) =
  max arr t

[<TestCase(1, 0.0, 7.0, 0.5, Result = 38.5)>]
[<TestCase(2, -6.0, 0.0, 1, Result = -6.0)>]
let ``TestIntegral`` (threadNumber : int) l r e : float  =
  defIntegral threadNumber (fun x -> x + 2.0) l r e 