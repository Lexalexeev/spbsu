// Homework 5
// Alekseev Aleksei, group 171.

open NUnit.Framework
open System.IO

// Task 35 (Task 37 + Task 38)
type ADTStack<'A> = Nil | Cons of 'A * ADTStack<'A>

type Stack<'A when 'A: equality> () = 
  class
    let mutable (stack : ADTStack<'A>) = Nil
    member this.isEmpty = (stack = Nil)
    member this.push elem = stack <- Cons (elem, stack)
    member this.pop () =
      match stack with
      | Nil               -> failwith "Stack is empty"
      | Cons (elem, next) ->
        stack <- next
        elem
    member this.top () = 
      match stack with
      | Nil               -> failwith "Stack is empty"
      | Cons (elem, next) -> elem
    end

// Task 37
let GetExpression (inputFile : string) (outputFile : string) =
  use inputStream = new StreamReader(inputFile)
  use outputStream = new StreamWriter(outputFile)
  let InStr = inputStream.ReadLine()
  let Vstr = inputStream.ReadLine()
  let priority operator =
    match operator with
    | "+" -> 1 | "-" -> 1 | "*" -> 2 | "/" -> 2 | "%" -> 2 | "^" -> 3 |  _  -> 0    
  let mutable token = ""
  let mutable tokens = [] 
  for i = 0 to InStr.Length - 1 do
    let ch = InStr.[i] 
    if System.Char.IsDigit(ch) then token <- token + ch.ToString()
    else
      match ch with
      | ' ' ->
        if System.Char.IsDigit(InStr.[i - 1]) then
          tokens <- List.append tokens [token;]
          token <- ""
      | '-' ->
        if System.Char.IsDigit(InStr.[i + 1]) then token <- "-"
        else tokens <- List.append tokens ["-";]
      | '(' -> tokens <- List.append tokens ["(";]
      | ')' ->
        if token.Length > 0 then
          tokens <- List.append tokens [token;]
          token <- "" 
        tokens <- List.append tokens [")";]
      |  _  -> tokens <- List.append tokens [ch.ToString();]  
  if token.Length > 0 then tokens <- List.append tokens [token;]
 
  let stack = new Stack<string>()
  for token in tokens do
    if token.Length > 1 || System.Char.IsDigit(token.[0]) then 
      outputStream.WriteLine(token)
    else
      match token with
      | "(" -> stack.push(token)
      | ")" ->
        while stack.top() <> "(" && (not stack.isEmpty) do
          outputStream.WriteLine(stack.pop())
        ignore(stack.pop())
      | _   ->
        while not stack.isEmpty 
          && (priority(stack.top()) >= priority(token) && priority(token) < 3
            || (priority(stack.top()) >  priority(token) && priority(token) = 3))
              do outputStream.WriteLine(stack.pop())
        stack.push(token)
  while not stack.isEmpty do outputStream.WriteLine(stack.pop())

// Task 38
let Calculate (inputFile : string) (outputFile : string) =
  use inputStream = new StreamReader(inputFile)
  use outputStream = new StreamWriter(outputFile)
  let stack = new Stack<int>()
  while not inputStream.EndOfStream do
    let InStr = inputStream.ReadLine()
    if InStr.Length > 0 then
      if InStr.Length > 1 || System.Char.IsDigit(InStr.[0]) then 
        stack.push(System.Convert.ToInt32(InStr))   
      else
        let a = stack.pop()
        let b = stack.pop()
        match InStr with
        | "+" -> stack.push(b + a)
        | "-" -> stack.push(b - a)
        | "*" -> stack.push(b * a)
        | "/" -> stack.push(b / a)
        | "%" -> stack.push(b % a)
        | "^" -> stack.push(pown b a)
        | _   -> failwith "Incorrect operator"
  outputStream.WriteLine(stack.pop())

let write (str : string) (file : string) =
  use stream = new StreamWriter(file)
  stream.WriteLine(str)
let read (file : string) =
  use stream = new StreamReader(file)
  stream.ReadToEnd()

// Tests (Task 35)
[<TestCase ("((1 + 2) ^ 2) * 4 - 6 / 3 + 5 % 4 + (-20)", Result = "15")>] // все операции
[<TestCase ("1 - 2 - 3", Result = "-4")>] // обязательный тест
[<TestCase ("3 ^ 1 ^ 2", Result = "3")>] // обязательный тест

let ``Test`` (expression : string) =
  let inputFile = "test.in"
  let outputFile = "test.out"
  write expression inputFile
  GetExpression inputFile outputFile
  write (read(outputFile)) inputFile     
  Calculate inputFile outputFile
  (read(outputFile)).TrimEnd('\r', '\n') 

// Tests (Task 37)
[<TestCase ("1 + 3", Result = "1\r\n3\r\n+")>]
[<TestCase ("1 - 2 - 3", Result = "1\r\n2\r\n-\r\n3\r\n-")>]
[<TestCase ("3 ^ 1 ^ 2", Result = "3\r\n1\r\n2\r\n^\r\n^")>]

let ``TestExpr`` (expression : string) =
  let inputFile = "test.in"
  let outputFile = "test.out"
  write expression inputFile
  GetExpression inputFile outputFile
  (read(outputFile)).TrimEnd('\r', '\n') 

// Tests (Task 38)
[<TestCase ("1\r\n3\r\n+\r\n", Result = "4")>]
[<TestCase ("3\r\n1\r\n2\r\n^\r\n^\r\n", Result = "3")>]

let ``TestCalc`` (expression : string) =
  let inputFile = "test.in"
  let outputFile = "test.out"
  write expression inputFile    
  Calculate inputFile outputFile
  (read(outputFile)).TrimEnd('\r', '\n')

[<EntryPoint>]
let main argv =  
  0