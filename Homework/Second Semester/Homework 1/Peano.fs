// Homework 1
// Alekseev Aleksei, group 171.


type Peano = Zero | S of Peano


// Task 1
let rec minus a b =
  match a, b with
  | Zero, _ -> Zero
  | a, Zero -> a
  | S a, S b -> minus a b


// Task 2
let rec toInt a =
  match a with
  | Zero -> 0
  | S a -> 1 + toInt a


// Task 3
let rec plus a b =
  match a with
  | Zero -> b
  | S a -> S (plus a b)

let rec mult a b =
  match a, b with
  | _, Zero -> Zero
  | Zero, _ -> Zero
  | a, S b -> plus (mult a b) a


// Task 4
let rec exp a b =
  match a, b with
  | a, Zero -> S Zero
  | Zero, _ -> Zero
  | a, S b -> mult (exp a b) a

[<EntryPoint>]
let main argv = 
  let arg1 = (S (S (S (S (S Zero)))))
  let arg2 = (S (S Zero))
  printf "arg1: %A\n"   arg1
  printf "arg2: %A\n\n" arg2
  printf "minus: %A\n"   (toInt (minus arg1 arg2))
  printf "toInt: %d, %d\n"   (toInt arg1) (toInt arg2)
  printf "mult: %A\n"   (toInt (mult arg1 arg2))
  printf "exp: %A\n"   (toInt (exp arg1 arg2))
  0 
