// Homework 3
// Alekseev Aleksei, group 171.


// Task 20
type IGraph<'A> = 
 interface
   abstract Size : int
   abstract Val : int -> 'A
   abstract Edge : int -> int -> bool
 end 


// Task 21
type MatrixGraph<'A> (verges: list<int*int>, numberOfNods, vals: 'A[]) =
  class
    let nodeVals = vals
    let adjMatrix = Array2D.zeroCreate numberOfNods numberOfNods
    do 
      for (n1, n2) in verges do Array2D.set adjMatrix n1 n2 1
    interface IGraph<'A> with
      override s.Edge a b =
        if (adjMatrix.[a, b] = 1) then true else false
      override s.Size = 
        Array2D.length1 adjMatrix
      override s.Val a = 
        nodeVals.[a]
    end
  end


// Task 22
// Task 23
// Task 24


// Task 25
type IGraphMarked<'A> =
  interface
    inherit IGraph<'A>
    abstract EdgeLength : int -> int -> int
  end


// Task 26


// Task 27
type IList<'A> =
    interface
        abstract insFront : 'A -> unit
        abstract insBack : 'A -> unit
        abstract insTo : 'A -> int -> unit
        abstract delFront : unit -> unit
        abstract delBack : unit -> unit
        abstract delFrom : int -> unit
        abstract Search : ('A -> bool) -> 'A
        abstract Concat : IList<'A> -> unit
        abstract Print : unit -> unit
    end

// Task 28
// Task 29

[<EntryPoint>]
let main argv = 
    0 
