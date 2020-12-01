module RoverModule

type Coordinate = { X: int; Y: int }

type Command = string
    
type Direction = 
    | North = 1
    | East = 2
    | South = 3
    | West = 4

let toRight (currentDirection: Direction): Direction =
    currentDirection 
        |> int 
        |> (fun x -> x + 1)
        |> (fun x -> x % 4)
        |> enum<Direction>

let toLeft (currentDirection: Direction): Direction =
   currentDirection 
       |> int 
       |> (fun x -> x - 1)
       |> (fun x -> if x = 0 then 4 else x)
       |> enum<Direction>

type Rover(position: Coordinate, direction: Direction) = 
    member _.Position = position
    member _.Direction = direction
    member _.changeDirection(newDirection: Direction): Rover = Rover(position, newDirection)
    member _.changePosition(newPosition: Coordinate): Rover = Rover(newPosition, direction)
    new() = Rover({X= 0 ; Y=0}, Direction.North)

let determinateNextPosition (position: Coordinate) (direction: Direction) (number: int): Coordinate =
    match direction with
    | Direction.North -> {X= position.X ; Y=position.Y + number}
    | Direction.East -> {X = position.X + number ; Y = position.Y}
    | Direction.South -> {X = position.X ; Y = position.Y - number}
    | Direction.West -> {X = position.X - number ; Y = position.Y}
    | _ -> position

let moveForward (rover: Rover): Rover =
    1 
    |> determinateNextPosition rover.Position rover.Direction 
    |> rover.changePosition

let moveBackward (rover: Rover): Rover =
    -1 
    |> determinateNextPosition rover.Position rover.Direction 
    |> rover.changePosition

let rotateLeft (rover: Rover): Rover =
    rover.Direction
        |> toLeft 
        |> rover.changeDirection

let rotateRight (rover: Rover): Rover =
    rover.Direction
        |> toRight 
        |> rover.changeDirection

let execute (rover: Rover) (command: Command): Rover = 
    match command with
    | "R" -> rover |> rotateRight
    | "L" -> rover |> rotateLeft
    | "F" -> rover |> moveForward
    | "B" -> rover |> moveBackward
    | _ -> rover

let first (command: Command): Command =
     command.Substring(0, 1)

let others (command: Command): Command =
    command.Substring(1)

let rec executeCommand (command: Command) (rover: Rover): Rover = 
    match command.Length with
    | 1 -> command |> first |> execute rover
    | _ -> command
           |> first
           |> execute rover
           |> executeCommand (command |> others)
    
