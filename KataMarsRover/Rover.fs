module RoverModule

type Coordinate = { X: int; Y: int }

type Command = string

type Direction = 
    | North 
    | South
    | East
    | West

type Rover(position: Coordinate, direction: Direction) = 
    member this.Position = position
    member this.Direction = direction
    new() = Rover({X= 0 ; Y=0}, Direction.North)

let moveForward (rover: Rover): Rover =
    Rover({X= rover.Position.X ; Y=rover.Position.Y + 1}, Direction.North)

let executeCommand (command: Command) (rover: Rover): Rover = 
    match command with
    | "F" -> rover |> moveForward
    | _ -> rover