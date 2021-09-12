This is a webAPI project using .NET Core 5

It is a partial implementation of Battleship board game.

To run the solution, set Battleship project as startup project in visual studio

This project has three API calls:
- [POST]api/Board/Create : Create the board with default size 10x10. (0,0) coordinates is at the top left, while (9,9) is bottom right.
Returns width and height values of the board

- [POST]api/Board/AddShip: Receives three parameters: Coordinates, Length, and Orientation.
The ship always starts at Coordinates, and extending by Length - 1 to the right (if Orientation is horizontal), or down (if Orientation is vertical). 
The ship dimension is 1xLength.
Cannot put ship if it's outside the board coordinates or overlaps with another ship.
Returns Ship state (Alive/Sunk) and Ship number

- [POST]api/Board/Attack: Receives a Coordinates
Attack a coordinates. Returns Hit if a ship is at the specified coordinates, otherwise a Miss. Will return error if coordinates is outside of the game board.

Please consult the swagger page for schema sample (/swagger/index.html)