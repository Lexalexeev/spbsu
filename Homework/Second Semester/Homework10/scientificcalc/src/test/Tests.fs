// Homework 10
// Alekseev Aleksei, group 171.

module Tests

open System
open System.Windows.Forms
open System.Drawing
open Window
open Calc
open NUnit.Framework

[<TestCase ("(35 - 15) * 2 + (5 * (-20) + 10) / 3", Result = "10")>]
[<TestCase ("1 - 100", Result = "-99")>]
[<TestCase ("1 - 2 - 3", Result = "-4")>]
[<TestCase ("2 + 2 * 2", Result = "6")>]
[<TestCase ("5 + (-5)", Result = "0")>]

let ``Test`` (str : string) = 
  input.Text <- str
  string (Calculate(GetExpression(input.Text)))
