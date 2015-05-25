// Homework 10
// Alekseev Aleksei, group 171.

module Plotter

open System
open System.Windows.Forms
open System.Drawing
open System.Windows.Forms.DataVisualization.Charting
open Calc

let labelY =
  let label = new Label()
  label.Font <- new Font("Arial", 18.0f)
  label.Text <- "y = "
  label.Size <- new Size(50,40)
  label.Location <- new Point(60,30)
  label.TextAlign <- ContentAlignment.TopCenter
  label

let input =
  let txtbox = new TextBox()
  txtbox.BorderStyle <- BorderStyle.FixedSingle
  txtbox.Text <- "x ^ 2"
  txtbox.Size <- new Size(200,40)
  txtbox.Location <- new Point(110,30)
  txtbox.TextAlign <- HorizontalAlignment.Center
  txtbox.Font <- new Font("Arial", 14.0f)
  txtbox

let inputL =
  let txtbox = new TextBox()
  txtbox.BorderStyle <- BorderStyle.FixedSingle
  txtbox.Text <- "-9"
  txtbox.Size <- new Size(50,40)
  txtbox.Location <- new Point(60,80)
  txtbox.TextAlign <- HorizontalAlignment.Center
  txtbox.Font <- new Font("Arial", 14.0f)
  txtbox

let inputR =
  let txtbox = new TextBox()
  txtbox.BorderStyle <- BorderStyle.FixedSingle
  txtbox.Text <- "10"
  txtbox.Size <- new Size(50,40)
  txtbox.Location <- new Point(260,80)
  txtbox.TextAlign <- HorizontalAlignment.Center
  txtbox.Font <- new Font("Arial", 14.0f)
  txtbox

let labelX =
  let label = new Label()
  label.Font <- new Font("Arial", 18.0f)
  label.Text     <-  "<= x <="
  label.Size     <- new Size (100, 40)
  label.Location <- System.Drawing.Point(135, 80)
  label.TextAlign <- ContentAlignment.TopCenter
  label

let exitButton =
  let btn = new Button()
  btn.Size <- new Size(80,30)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- "Exit"
  btn.Location <- new Point(230,130)
  btn.BackColor <- Color.Tomato
  btn.Click.Add(fun _ -> Application.Exit())
  btn

let drawAction () = 
  let chart = new Chart (Dock = DockStyle.Fill)
  let form  = new Form ()
  form.Size <- new Size(400,400)
  form.Visible <- true
  chart.ChartAreas.Add (new ChartArea("MainArea"))
  form.Controls.Add (chart)
  let mutable series = new Series (ChartType = SeriesChartType.Line)
  chart.Series.Add(series)
  series.Color <- Color.Blue
  let l = int inputL.Text
  let r = int inputR.Text
  let mutable y = 0
  let mutable temp = "x "
  for i in l .. 1 .. r do
    try
      y <- Calculate (GetExpression input.Text (temp + string i))
      ignore(series.Points.AddXY (i, y))
    with
      | DivisionByZero -> 
        series <- new Series (ChartType = SeriesChartType.Line)
        chart.Series.Add(series)
        series.Color <- Color.Blue
 
let startButton =
  let btn = new Button()
  btn.Size <- new Size (80, 30)
  btn.Font <- new Font("Arial", 14.0f)
  btn.Text <- "Start"
  btn.Location <- System.Drawing.Point(60, 130)
  btn.BackColor <- Color.LightGreen
  btn.Click.Add (fun e -> drawAction())
  btn

let mainForm =
  let form = new Form()
  form.Visible <- false
  form.Text <- "Plotter by Aleksei Alekseev"
  form.Size <- new Size(400,400)
  form.BackColor <- Color.Silver
  form.FormBorderStyle <- FormBorderStyle.FixedDialog
  form.Controls.Add(input)
  form.Controls.Add(inputL)
  form.Controls.Add(inputR)
  form.Controls.Add(labelX)
  form.Controls.Add(labelY)
  form.Controls.Add(exitButton)
  form.Controls.Add(startButton)
  if form.Size <> Size(400,400) then form.Size <- new Size(400,400)
  form

[<EntryPoint>]
let main argv = 
  mainForm.Visible <- true
  Application.Run() 
  0