// Homework 7
// Alekseev Aleksei, group 171.

module Tests

open NUnit.Framework
open Workflow

[<TestCase(5,  Result = 0)>]
[<TestCase(1,  Result = 0)>]
[<TestCase(3,  Result = 1)>]
[<TestCase(15, Result = 10)>]
let ``Test1`` (n) =
  ring n {
    let! a = 2 * 3
    let! b = 4
    return a + b
  }

[<Test>]
let ``Test2`` () =
  let example =
    ring 7 {
      let! a = 2 * 3
      let! b = 9999999 * 7777778
      return a * pown b 4
    }
  Assert.AreEqual(5, example)

[<TestCase(9,  Result = 8)>]
[<TestCase(13, Result = 3)>]
let ``Test3`` (n) =
  ring n {
    let! a = -7 + (-3)
    return a
  }