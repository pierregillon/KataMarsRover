module Tests

open System
open Xunit
open RoverModule
open FluentAssertions

[<Fact>]
let ``Point to north by default`` () =
    Assert.Equal(Rover().Direction, Direction.North)

[<Fact>]
let ``Is 0,0 by default`` () =
    let position = Rover().Position;
    Assert.Equal(0, position.X)
    Assert.Equal(0, position.Y)

[<Fact>]
let ``Rotating right do not change position`` () =
    let rover = Rover() |> executeCommand "R"
    Assert.Equal(0, rover.Position.X)
    Assert.Equal(0, rover.Position.Y)

[<Fact>]
let ``Rotating right when facing north do change direction to east`` () =
    let rover = Rover() |> executeCommand "R"
    Assert.Equal(Direction.East, rover.Direction)
    
[<Fact>]
let ``Rotating left when facing north do change direction to west`` () =
    let rover = Rover() |> executeCommand "L"
    Assert.Equal(Direction.West, rover.Direction)
    
[<Fact>]
let ``Rotating right when facing east do change direction to south`` () =
    let rover = Rover({X= 0; Y=0}, Direction.East) |> executeCommand "R" 
    Assert.Equal(Direction.South, rover.Direction)

[<Fact>]
let ``Rotating right when facing west do change direction to south`` () =
    let rover = Rover({X= 0; Y=0}, Direction.West) |> executeCommand "R" 
    Assert.Equal(Direction.North, rover.Direction)

[<Fact>]
let ``7 rotations right when facing west do change direction to south`` () =
    let rover = Rover({X= 0; Y=0}, Direction.West) |> executeCommand "RRRRRRR" 
    Assert.Equal(Direction.South, rover.Direction)

[<Fact>]
let ``2 rotations right and 2 rotations left when facing south do not change direction`` () =
    let rover = Rover({X= 0; Y=0}, Direction.South) |> executeCommand "RRLL" 
    Assert.Equal(Direction.South, rover.Direction)

[<Fact>]
let ``Move forward twice and backward twice stay at the same position`` () =
    let rover = Rover({X= 1; Y=1}, Direction.South) |> executeCommand "FFBB" 
    Assert.Equal({X= 1; Y=1}, rover.Position)

[<Fact>]
let ``Turn left and move forward twice when facing North`` () =
    let rover = Rover({X=1; Y=1}, Direction.North) |> executeCommand "LFF" 
    Assert.Equal({X=(-1); Y=1}, rover.Position)

[<Fact>]
let ``Move forward update position`` () =
    let rover = Rover() |> executeCommand "F"
    Assert.Equal(rover.Position.X, 0)
    Assert.Equal(rover.Position.Y, 1)




//You are given the initial starting point (x,y) of a rover and the direction (N,S,E,W) it is facing.
//The rover receives a character array of commands.
//Implement commands that move the rover forward/backward (f,b).
//Implement commands that turn the rover left/right (l,r).
//Implement wrapping from one edge of the grid to another. (planets are spheres after all)
//Implement obstacle detection before each move to a new square. If a given sequence of commands encounters an obstacle, the rover moves up to the last possible point and reports the obstacle.


//https://fsharpforfunandprofit.com/posts/key-concepts/