// Homework 5
// Alekseev Aleksei, group 171.

// С нуля написать не смог, разобрал всё с помощью одногруппника.

open NUnit.Framework
open System.IO

// Stack

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

let GetExpression () =
    use inputStream = new StreamReader("test.in")
    use outputStream = new StreamWriter("test.out")
    let InStr = inputStream.ReadToEnd()

    let priority operator =
        match operator with
        | "+" -> 1
        | "-" -> 1
        | "*" -> 2
        | "/" -> 2
        | "%" -> 2
        | "^" -> 3
        |  _  -> 0
        
 

    let mutable token = ""
    let mutable tokens = []
    
    for i = 0 to InStr.Length - 3 do
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

let Calculate () =
   use inputStream = new StreamReader("test.in")
   use outputStream = new StreamWriter("test.out")
    
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
            | "+" -> stack.push(a + b)
            | "-" -> stack.push(b - a)
            | "*" -> stack.push(a * b)
            | "/" -> stack.push(b / a)
            | "%" -> stack.push(b % a)
            | "^" ->
               let rec exp elem p =
                  match p with
                  | 0 -> 1
                  | 1 -> elem
                  | p -> elem * (exp elem (p - 1))
               if a >= 0 then stack.push(exp b a)
               else stack.push(1 / (exp b -a))
            | _   -> failwith "Incorrect operator"
     
   outputStream.WriteLine(stack.pop())



let write (str : string) =
    use stream = new StreamWriter("test.in")
    stream.WriteLine(str)


let read () =
    use stream = new StreamReader("test.out")
    stream.ReadToEnd()


[<TestCase ("0 + 0", Result = "0")>]
[<TestCase ("5 * 0", Result = "0")>]
[<TestCase ("0 + 1", Result = "1")>]
[<TestCase ("-1 + 1", Result = "0")>]
[<TestCase ("20 - 10", Result = "10")>]
[<TestCase ("12 * 4", Result = "48")>]
[<TestCase ("56 / 6", Result = "9")>]
[<TestCase ("388 % 10", Result = "8")>]
[<TestCase ("5 ^ 2", Result = "25")>]
[<TestCase ("5 ^ (-2)", Result = "0")>]
[<TestCase ("(5 + (-4) * 3 + 2 ^ 3) - 1", Result = "0")>]
[<TestCase ("1 + 99999999", Result = "100000000")>]
[<TestCase ("1337 ^ 0", Result = "1")>]
[<TestCase ("20 - 30", Result = "-10")>]
[<TestCase ("((856 + 145) - 1000) ^ 8", Result = "1")>]
[<TestCase ("15 - (54 / 9 * 3 - 15 % 10) + 120 / (22 - 12) ^ 2", Result = "3")>]

let ``Test`` (expression : string) =
    write(expression)
    GetExpression()
    write(read())       
    Calculate ()
    (read()).TrimEnd('\r', '\n') 

[<EntryPoint>]
let main argv =  
   0
