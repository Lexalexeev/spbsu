// Homework 10
// Alekseev Aleksei, group 171.

module Window

open System
open System.Windows.Forms
open System.Drawing
open Calc

exception IncorrectString
exception IncorrectOperator

let input =
  let txtbox = new Label()
  txtbox.BorderStyle <- BorderStyle.FixedSingle
  txtbox.Text <- "0"
  txtbox.Size <- new Size(620,40)
  txtbox.Location <- new Point(40,20)
  txtbox.TextAlign <- ContentAlignment.MiddleRight
  txtbox.Font <- new Font("Arial", 14.0f)
  txtbox

let exitButton =
  let btn = new Button()
  btn.Size <- new Size(60,120)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- "Exit"
  btn.Location <- new Point(600,210)
  btn.BackColor <- Color.Tomato
  btn.Click.Add(fun _ -> Application.Exit())
  btn

let digitButton i =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- string i
  let mutable x = 40
  let mutable y = 280
  match i with
  | 2 | 5 | 8 -> x <- x + 80
  | 3 | 6 | 9 -> x <- x + 160
  | _ -> x <- 40
  match i with
  | 1 | 2 | 3 -> y <- y - 70
  | 4 | 5 | 6 -> y <- y - 140
  | 7 | 8 | 9 -> y <- y - 210
  | _ -> y <- 280
  btn.Location <- new Point(x,y)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> if (input.Text = "0") then input.Text <- btn.Text else input.Text <- input.Text + btn.Text)
  btn

let operButton op =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- string op
  btn.BackColor <- Color.LightGray
  match op with
  | '/' -> btn.Location <- new Point(280,70)
  | '*' -> btn.Location <- new Point(280,140)
  | '-' -> btn.Location <- new Point(280,210)
  | '+' -> btn.Location <- new Point(280,280)
  | _ -> raise IncorrectOperator
  btn.Click.Add(fun _ -> 
    let mutable str = input.Text 
    let s = str.[str.Length - 1]
    if System.Char.IsDigit(s) || s = ')'
    then input.Text <- input.Text + " " + string op + " "
    if (op = '-') && (s = '(')
    then input.Text <- input.Text + string op
    else if s = ' ' then
      str <- ""
      for i = 0 to input.Text.Length - 3 do
        str <- str + string input.Text.[i]
      str <- str + string op + " "
      input.Text <- str)
  btn

let equalButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- "="
  btn.Location <- new Point(200,280)
  btn.BackColor <- Color.LightBlue
  btn.Click.Add(fun _ -> 
    let s = input.Text
    let mutable lbkt = 0
    let mutable rbkt = 0
    for i = 0 to s.Length - 1 do
      if s.[i] = ',' then input.Text <- "0"
      if s.[i] = '(' then lbkt <- lbkt + 1
      if s.[i] = ')' then rbkt <- rbkt + 1
    if lbkt <> rbkt then 
      raise IncorrectString
    match s.[s.Length-2] with
    | '+' | '-' | '*' | '/' | '(' ->  raise IncorrectString
    | _ ->
    input.Text <- string (Calculate(GetExpression(input.Text))))
  btn

let zeroButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- "000"
  btn.Location <- new Point(120,280)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> 
    if System.Char.IsDigit(input.Text.[input.Text.Length - 1]) && input.Text <> "0"
    then input.Text <- input.Text + "000")
  btn

let lbktButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- "("
  btn.Location <- new Point(360,140)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> 
    let str = input.Text 
    let s = str.[str.Length - 1]
    if (s = '0') && (str.Length = 1) then input.Text <- "(" else 
    if (not(System.Char.IsDigit(s)) && s <> ')')
    then input.Text <- input.Text + "(")
  btn

let rbktButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- ")"
  btn.Location <- new Point(440,140)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> 
    let str = input.Text 
    let mutable lbkt = 0
    let mutable rbkt = 0
    for i = 0 to str.Length - 1 do
      if str.[i] = '(' then lbkt <- lbkt + 1
      if str.[i] = ')' then rbkt <- rbkt + 1
    if lbkt > rbkt then 
      let s = str.[str.Length - 1]
      if System.Char.IsDigit(s) || s = ')'
      then input.Text <- input.Text + ")")
  btn

let rec fact n =
  match n with
  | 0 -> 1
  | _ -> n * fact (n - 1)

let factButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- "x!"
  btn.Location <- new Point(600,70)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> 
    let str = input.Text 
    let mutable f = 0
    for i = 0 to str.Length - 1 do
      if not (System.Char.IsDigit(str.[i])) then f <- 1
    if f = 0 then input.Text <- (fact(int input.Text)).ToString()
    else input.Text <- "0")
  btn

let fracButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- "1/x"
  btn.Location <- new Point(520,140)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> 
    let str = input.Text 
    let mutable f = 0
    for i = 1 to str.Length - 1 do
      if not (System.Char.IsDigit(str.[i])) then f <- 1
    if f = 0 then 
      if input.Text = "0" then input.Text <- "0"
      else input.Text <- (1.0/(Double.Parse(input.Text))).ToString()
    else input.Text <- "0")
  btn

let signButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- "±"
  btn.Location <- new Point(520,70)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> 
    let str = input.Text 
    let mutable f = 0
    for i = 1 to str.Length - 1 do
      if not (System.Char.IsDigit(str.[i])) then f <- 1
    if f = 0 then input.Text <- (int input.Text * (-1)).ToString()
    else input.Text <- "0")
  btn

let logButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- "log"
  btn.Location <- new Point(520,280)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> 
    let str = input.Text 
    let mutable f = 0
    for i = 0 to str.Length - 1 do
      if not (System.Char.IsDigit(str.[i])) then f <- 1
    if f = 0 then input.Text <- System.Math.Log(Double.Parse(input.Text)).ToString()
    else input.Text <- "0")
  btn

let sqrButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- "x²"
  btn.Location <- new Point(520,210)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> 
    let str = input.Text 
    let mutable f = 0
    for i = 1 to str.Length - 1 do
      if not (System.Char.IsDigit(str.[i])) then f <- 1
    if f = 0 then input.Text <- string (int input.Text * int input.Text)
    else input.Text <- "0")
  btn

let sqrtButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- "√x"
  btn.Location <- new Point(600,140)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> 
    let str = input.Text 
    let mutable f = 0
    for i = 0 to str.Length - 1 do
      if not (System.Char.IsDigit(str.[i])) then f <- 1
    if f = 0 then input.Text <- System.Math.Sqrt(Double.Parse(input.Text)).ToString()
    else input.Text <-"0")
  btn

let tgButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- "tg"
  btn.Location <- new Point(360,280)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> 
    let str = input.Text 
    let mutable f = 0
    for i = 1 to str.Length - 1 do
      if not (System.Char.IsDigit(str.[i])) then f <- 1
    if f = 0 then input.Text <- System.Math.Tan(Double.Parse(input.Text)).ToString()
    else input.Text <-"0")
  btn

let ctgButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- "ctg"
  btn.Location <- new Point(440,280)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> 
    let str = input.Text 
    let mutable f = 0
    for i = 1 to str.Length - 1 do
      if not (System.Char.IsDigit(str.[i])) then f <- 1
    if f = 0 then input.Text <- (1.0/System.Math.Tan(Double.Parse(input.Text))).ToString()
    else input.Text <-"0")
  btn

let sinButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- "sin"
  btn.Location <- new Point(360,210)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> 
    let str = input.Text 
    let mutable f = 0
    for i = 1 to str.Length - 1 do
      if not (System.Char.IsDigit(str.[i])) then f <- 1
    if f = 0 then input.Text <- System.Math.Sin(Double.Parse(input.Text)).ToString()
    else input.Text <- "0")
  btn

let cosButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- "cos"
  btn.Location <- new Point(440,210)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ ->
    let str = input.Text 
    let mutable f = 0
    for i = 1 to str.Length - 1 do
      if not (System.Char.IsDigit(str.[i])) then f <- 1
    if f = 0 then input.Text <- System.Math.Cos(Double.Parse(input.Text)).ToString()
    else input.Text <- "0")
  btn

let clearButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 12.0f)
  btn.Text <- "Сlear"
  btn.Location <- new Point(440,70)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> input.Text <- "0")
  btn

let backspaceButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- "←"
  btn.Location <- new Point(360,70)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> 
    if input.Text <> "0" then
      let mutable str = ""
      let mutable space = input.Text.Length - 2
      if space > 0 || space = 0 then     
        if input.Text.[space] = ' ' then space <- space - 1
      for i = 0 to space do
        str <- str + string input.Text.[i]
      if str = "" then str <- "0"
      input.Text <- str)
  btn

let mainForm =
  let form = new Form()
  form.Visible <- false
  form.Text <- "Calculator by Aleksei Alekseev"
  form.Size <- new Size(700,400)
  form.BackColor <- Color.Silver
  form.FormBorderStyle <- FormBorderStyle.FixedDialog
  form.Controls.Add(input)
  for i = 0 to 9 do form.Controls.Add(digitButton i)
  for c in [|'/';'*';'-';'+'|] do form.Controls.Add(operButton c)
  form.Controls.Add(zeroButton)
  form.Controls.Add(equalButton)
  form.Controls.Add(backspaceButton)
  form.Controls.Add(clearButton)
  form.Controls.Add(lbktButton)
  form.Controls.Add(rbktButton)
  form.Controls.Add(tgButton)
  form.Controls.Add(ctgButton)
  form.Controls.Add(sinButton)
  form.Controls.Add(cosButton)
  form.Controls.Add(sqrtButton)
  form.Controls.Add(sqrButton)
  form.Controls.Add(logButton)
  form.Controls.Add(signButton)
  form.Controls.Add(fracButton)
  form.Controls.Add(factButton)
  form.Controls.Add(exitButton)
  if form.Size <> Size(700,400) then form.Size <- new Size(700,400)
  form

[<EntryPoint>]
let main argv = 
  mainForm.Visible <- true
  Application.Run() 
  0