// Homework 8
// Alekseev Aleksei, group 171.

module Window

open System
open System.Windows.Forms
open System.Drawing
open Calc

let input =
  let txtbox = new TextBox()
  txtbox.AppendText("0")
  txtbox.Size <- new Size(460,0)
  txtbox.Location <- new Point(40,20)
  txtbox.TextAlign <- HorizontalAlignment.Right
  txtbox.Font <- new Font("Arial", 12.0f)
  txtbox

let exitButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Text <- "Exit"
  btn.Location <- new Point(440,280)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> Application.Exit())
  btn

let digitButton i =
  let btn = new Button()
  btn.Size <- new Size(60,50)
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
  btn.Text <- string op
  btn.BackColor <- Color.LightGray
  match op with
  | '/' -> btn.Location <- new Point(280,70)
  | '*' -> btn.Location <- new Point(280,140)
  | '-' -> btn.Location <- new Point(280,210)
  | '+' -> btn.Location <- new Point(280,280)
  | _ -> failwith ""
  btn.Click.Add(fun _ -> 
    let mutable str = input.Text 
    let s = str.[str.Length - 1]
    if System.Char.IsDigit(s) || s = ')'
    then input.Text <- input.Text + " " + string op + " "
    if (op = '-') && (s = '(')
    then input.Text <- input.Text + string op
    else if s = ' ' then
      str <- ""
      for i = 0 to input.TextLength - 3 do
        str <- str + string input.Text.[i]
      str <- str + string op + " "
      input.Text <- str)
  btn

let equalButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Text <- "="
  btn.Location <- new Point(200,280)
  btn.BackColor <- Color.LightBlue
  btn.Click.Add(fun _ -> input.Text <- string (Calculate(GetExpression(input.Text))))
  btn

let dotButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Text <- "."
  btn.Enabled <- false
  btn.Location <- new Point(120,280)
  btn.BackColor <- Color.LightGray
  btn

let lbktButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Text <- "("
  btn.Location <- new Point(360,140)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> 
    let str = input.Text 
    let s = str.[str.Length - 1]
    if (s = '0') then input.Text <- "(" else 
    if (not(System.Char.IsDigit(s)) && s <> ')')
    then input.Text <- input.Text + "(")
  btn

let rbktButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Text <- ")"
  btn.Location <- new Point(440,140)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> 
    let str = input.Text 
    let s = str.[str.Length - 1]
    if System.Char.IsDigit(s) || s = ')'
    then input.Text <- input.Text + ")")
  btn

let sqrtButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Text <- "sqrt"
  btn.Enabled <- false
  btn.Location <- new Point(360,280)
  btn.BackColor <- Color.LightGray
  btn

let sinButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Text <- "sin"
  btn.Enabled <- false
  btn.Location <- new Point(360,210)
  btn.BackColor <- Color.LightGray
  btn

let cosButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Text <- "cos"
  btn.Enabled <- false
  btn.Location <- new Point(440,210)
  btn.BackColor <- Color.LightGray
  btn

let clearButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Text <- "Сlear"
  btn.Location <- new Point(440,70)
  btn.BackColor <- Color.LightGray
  btn.Click.Add(fun _ -> input.Text <- "0")
  btn

let backspaceButton =
  let btn = new Button()
  btn.Size <- new Size(60,50)
  btn.Text <- "<-"
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
  form.Size <- new Size(560,400)
  form.BackColor <- Color.Silver
  form.FormBorderStyle <- FormBorderStyle.FixedDialog
  form.Controls.Add(input)
  for i = 0 to 9 do form.Controls.Add(digitButton i)
  for c in [|'/';'*';'-';'+'|] do form.Controls.Add(operButton c)
  form.Controls.Add(dotButton)
  form.Controls.Add(equalButton)
  form.Controls.Add(backspaceButton)
  form.Controls.Add(clearButton)
  form.Controls.Add(lbktButton)
  form.Controls.Add(rbktButton)
  form.Controls.Add(sinButton)
  form.Controls.Add(cosButton)
  form.Controls.Add(sqrtButton)
  form.Controls.Add(exitButton)
  if form.Size <> Size(560,400) then form.Size <- new Size(560,400)
  form

[<EntryPoint>]
let main argv = 
  mainForm.Visible <- true
  Application.Run() 
  0