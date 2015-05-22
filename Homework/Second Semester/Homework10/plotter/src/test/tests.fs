// Homework 10
// Alekseev Aleksei, group 171.

module Tests

open Calc
open NUnit.Framework

[<TestCase ("(3 * x - 5 * a) ^ t - z","a 0 t 2 x 2 z 6", Result = "30")>]
[<TestCase ("5 * a - b","a -1 b 2", Result = "-7")>]
[<TestCase ("5 + 2 * 3 - 16 / 4 + 6 % 3 - (-2) ^ 2","", Result = "3")>]
[<TestCase ("1 - 2 - 3","", Result = "-4")>]
[<TestCase ("3 ^ 1 ^ 2","", Result = "3")>]
let ``Test`` (instr : string) (vstr : string) = 
  string (Calculate(GetExpression instr vstr))
