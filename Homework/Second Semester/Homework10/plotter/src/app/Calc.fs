// Homework 10
// Alekseev Aleksei, group 171.

module Calc

open System.IO

type ADTStack<'A> = Nil | Cons of 'A * ADTStack<'A>

exception ListIsEmpty
exception IncorrectOperator
exception DivisionByZero

type Stack<'A when 'A: equality> () = 
  class
    let mutable (stack : ADTStack<'A>) = Nil
    member this.isEmpty = (stack = Nil)
    member this.push elem = stack <- Cons (elem, stack)
    member this.pop () =
      match stack with
      | Nil               -> raise ListIsEmpty
      | Cons (elem, next) ->
        stack <- next
        elem
    member this.top () = 
      match stack with
      | Nil               -> raise ListIsEmpty
      | Cons (elem, next) -> elem
    end

let GetExpression (instr : string) (vstr : string) =
  let priority operator =
    match operator with
    | "+" -> 1 | "-" -> 1 | "*" -> 2 | "/" -> 2 | "%" -> 2 | "^" -> 3 |  _  -> 0    
  let mutable token = ""
  let mutable tokens = []
  let mutable outStr = ""  
  let replace(a) =
    let mutable i = 0
    let mutable res = ""
    let mutable ch = vstr.[i]
    while ch <> a do 
      i <- i + 1
      ch <- vstr.[i] 
    i <- i + 2  
    ch <- vstr.[i]
    while (System.Char.IsDigit(ch) || ch = '-') && i < vstr.Length - 1 do
      res <- res + ch.ToString()
      i <- i + 1
      ch <- vstr.[i]
    if ch.ToString() <> " " then res <- res + ch.ToString()
    res
  for i = 0 to instr.Length - 1 do
    let ch = instr.[i] 
    if System.Char.IsLetter(ch) then token <- token + replace(ch)
    else
      if System.Char.IsDigit(ch) then token <- token + ch.ToString()
      else
        match ch with
        | ' ' ->
          if System.Char.IsDigit(instr.[i - 1]) || System.Char.IsLetter(instr.[i - 1]) then
            tokens <- List.append tokens [token;]
            token <- ""
        | '-' ->
          if System.Char.IsDigit(instr.[i + 1]) || System.Char.IsLetter(instr.[i - 1]) then token <- "-"
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
      outStr <- outStr + token + " "
    else
      match token with
      | "(" -> stack.push(token)
      | ")" ->
        while stack.top() <> "(" && (not stack.isEmpty) do
          outStr <- outStr + stack.pop() + " "
        ignore(stack.pop())
      | _   ->
        while not stack.isEmpty 
          && (priority(stack.top()) >= priority(token) && priority(token) < 3
            || (priority(stack.top()) >  priority(token) && priority(token) = 3))
              do outStr <- outStr + stack.pop() + " "
        stack.push(token)
  while not stack.isEmpty do outStr <- outStr + stack.pop() + " "
  outStr

let Calculate (str : string) =
  let mutable temp = ""
  let stack = new Stack<int>()
  for i = 0 to str.Length - 2 do
    if str.[i] <> ' ' then temp <- temp + string str.[i]
    if str.[i + 1] = ' ' then
      if temp.Length > 0 then
        if temp.Length > 1 || System.Char.IsDigit(temp.[0]) then 
          stack.push(System.Convert.ToInt32(temp))  
          temp <- "" 
        else
          let a = stack.pop()
          let b = stack.pop()
          if (temp = "/") && (a = 0) then raise DivisionByZero
          match temp with
          | "+" -> stack.push(b + a)
          | "-" -> stack.push(b - a)
          | "*" -> stack.push(b * a)
          | "/" -> stack.push(b / a)
          | "%" -> stack.push(b % a)
          | "^" -> stack.push(pown b a)
          | _   -> raise IncorrectOperator
          temp <- ""
  stack.pop()