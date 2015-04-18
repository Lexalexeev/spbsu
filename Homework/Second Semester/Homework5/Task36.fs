// Homework 5
// Alekseev Aleksei, group 171.

open NUnit.Framework
open System.IO

// Task 36
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
  let replace(a) =
    let mutable i = 0
    let mutable res = ""
    let mutable ch = Vstr.[i]
    while ch <> a do 
      i <- i + 1
      ch <- Vstr.[i] 
    i <- i + 2  
    ch <- Vstr.[i]
    while (System.Char.IsDigit(ch) || ch = '-') && i < Vstr.Length - 1 do
      res <- res + ch.ToString()
      i <- i + 1
      ch <- Vstr.[i]
    if ch.ToString() <> " " then res <- res + ch.ToString()
    res
  for i = 0 to InStr.Length - 1 do
    let ch = InStr.[i] 
    if System.Char.IsLetter(ch) then token <- token + replace(ch)
    else
      if System.Char.IsDigit(ch) then token <- token + ch.ToString()
      else
        match ch with
        | ' ' ->
          if System.Char.IsDigit(InStr.[i - 1]) || System.Char.IsLetter(InStr.[i - 1]) then
            tokens <- List.append tokens [token;]
            token <- ""
        | '-' ->
          if System.Char.IsDigit(InStr.[i + 1]) || System.Char.IsLetter(InStr.[i - 1]) then token <- "-"
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

[<TestCase ("(3 * x - 5 * a) ^ t - z\r\na 0 t 2 x 2 z 6", Result = "30")>] // переменные
[<TestCase ("5 * a - b\r\na -1 b 2", Result = "-7")>] // значение переменной меньше нуля
[<TestCase ("5 + 2 * 3 - 16 / 4 + 6 % 3 - (-2) ^ 2", Result = "3")>] // все операции
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

[<EntryPoint>]
let main argv =  
  0
