module RoverModule

type Coordinate = { X: int; Y: int }

type Command = 
    | Right
    | Left
    | Forward
    | Backward

type Direction = 
    | North
    | East
    | South
    | West

let toRight (currentDirection: Direction): Direction =
    match currentDirection with
    | North -> East
    | East -> South
    | South -> West
    | West -> North      

let toLeft (currentDirection: Direction): Direction =
    match currentDirection with
    | North -> West
    | West -> South
    | South -> East
    | East -> North  

type Rover(position: Coordinate, direction: Direction) = 
    member _.Position = position
    member _.Direction = direction
    member _.changeDirection(newDirection: Direction): Rover = Rover(position, newDirection)
    member _.changePosition(newPosition: Coordinate): Rover = Rover(newPosition, direction)
    new() = Rover({ X= 0 ; Y=0 }, Direction.North)

let determinateNextPosition (position: Coordinate) (direction: Direction) (number: int): Coordinate =
    match direction with
    | North -> { position with Y = position.Y + number }
    | East -> { position with X = position.X + number }
    | South -> { position with Y = position.Y - number }
    | West -> { position with X = position.X - number }

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
    | Right -> rover |> rotateRight
    | Left -> rover |> rotateLeft
    | Forward -> rover |> moveForward
    | Backward -> rover |> moveBackward

let rec executeCommandList (commands: Command list) (rover: Rover): Rover = 
    match commands with
    | [] -> rover
    | head :: tail -> 
        head
        |> execute rover
        |> executeCommandList tail

let toCommand (command: char) : Command =
    match command with
    | 'R' -> Right
    | 'L' -> Left
    | 'F' -> Forward
    | 'B' -> Backward
    | _ -> failwith "Unable to convert command"

let toCommands commands =
    commands 
    |> Seq.toList
    |> List.map toCommand
        
let executeCommand (commands: string) (rover: Rover): Rover = 

    let execteCommandOnRover commands = executeCommandList commands rover

    commands 
    |> toCommands
    |> execteCommandOnRover