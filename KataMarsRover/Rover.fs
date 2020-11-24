module RoverModule

type Coordinate = { X: int; Y: int }

type ListCommand = string

type Command = string

type Direction = 
    | North = 1
    | East = 2
    | South = 3
    | West = 4

type Rover(position: Coordinate, direction: Direction) = 
    member _.Position = position
    member _.Direction = direction
    new() = Rover({X= 0 ; Y=0}, Direction.North)

let moveForward (rover: Rover): Rover =
    Rover({X= rover.Position.X ; Y=rover.Position.Y + 1}, Direction.North)

let toRight (currentDirection: Direction): Direction =
   match currentDirection with
   | Direction.North -> Direction.East
   | Direction.East -> Direction.South
   | Direction.South -> Direction.West
   | Direction.West -> Direction.North
   | _ -> currentDirection; 

let rotate (command: Command) (rover: Rover): Rover = 
    match command with
    | "R" -> Rover({X= rover.Position.X ; Y=rover.Position.Y}, rover.Direction |> toRight)
    | "L" -> Rover({X= rover.Position.X ; Y=rover.Position.Y}, Direction.West)
    | _ -> rover 

let execute (rover: Rover) (command: Command): Rover = 
    match command with
    | "R" -> rover |> rotate command
    | "L" -> rover |> rotate command
    | "F" -> rover |> moveForward
    | _ -> rover

let first (command: ListCommand): Command =
     command.Substring(0, 1)

let others (command: ListCommand): Command =
    command.Substring(1)

let rec executeCommand (command: ListCommand) (rover: Rover): Rover = 
    match command.Length with
    | 1 -> command |> first |> execute rover
    | _ -> command.Substring(0, 1)
           |> execute rover
           |> executeCommand (command |> others)
    
