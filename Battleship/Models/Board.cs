using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Models
{
    public class Board
    {
        private const int DEFAULT_WIDTH = 10;
        private const int DEFAULT_HEIGHT = 10;
        public int Width { get; private set; }
        public int Height { get; private set; }
        protected List<Ship> Ships { get; set; }
        protected List<Cell> Cells { get; set; }

        /// <summary>
        /// Create the board. Default board size is 10x10, with (0,0) at the top left and (9,9) at the bottom right.
        /// </summary>
        public Board()
        {
            Setup(DEFAULT_WIDTH, DEFAULT_HEIGHT);
        }

        /// <summary>
        /// Create the board with custom size. Coordinates (0,0) at the top left and (width-1, length-1) at the bottom right.
        /// </summary>
        public Board(int width, int height)
        {
            Setup(width, height);
        }

        private void Setup(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ApplicationException("Size not valid");

            Cells = new List<Cell>();

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    Cells.Add(new Cell(i, j));

            Width = width;
            Height = height;

            Ships = new List<Ship>();
        }

        /// <summary>
        /// Creates a new ship on the board. Ship is 1xlength dimension. The ship will extend from starting coordinates to the right (if alignment is horizontal) or down (vertical alignment)
        /// </summary>
        /// <param name="startingCoordinates"></param>
        /// <param name="length"></param>
        /// <param name="alignment"></param>
        /// <returns>Ship object</returns>
        public Ship AddShip(Coordinates startingCoordinates, int length, ShipAlignment alignment)
        {
            ValidateBoard();

            ValidateShipPosition(startingCoordinates, length, alignment);

            var newShip = new Ship(Ships.Count);
            Ships.Add(newShip);

            // Change the cells to occupied for the ship
            for (int i = 0; i < length; i++)
            {
                Cell cell = null;

                if (alignment == ShipAlignment.Horizontal)
                    cell = Cells.First(c => c.CellCoordinates.X == startingCoordinates.X + i && c.CellCoordinates.Y == startingCoordinates.Y);
                else
                    cell = Cells.First(c => c.CellCoordinates.X == startingCoordinates.X && c.CellCoordinates.Y == startingCoordinates.Y + i);
                
                cell.ShipNumber = newShip.ShipNumber;
                cell.State = CellState.Occupied;
            }
            return newShip;
        }

        /// <summary>
        /// Attack the specified coordinates. Will return Hit if part of a ship is located at the coordinates, otherwise it's a Miss
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns>Hit or Miss enum</returns>
        public AttackResult Attack(Coordinates coordinates)
        {
            ValidateBoard();

            var targetCell = Cells.FirstOrDefault(c => c.CellCoordinates.X == coordinates.X && c.CellCoordinates.Y == coordinates.Y);

            if (targetCell == null)
                throw new ApplicationException("target coordinates does not exists");

            if (targetCell.State == CellState.Occupied)
                return AttackResult.Hit;
            else
                return AttackResult.Miss;
        }

        private void ValidateShipPosition(Coordinates startingCoordinates, int length, ShipAlignment alignment)
        {
            // Check if coordinates are valid
            if (startingCoordinates.X < 0 || startingCoordinates.X > Width - 1)
                throw new ApplicationException("Starting X coordinates not valid");

            if (startingCoordinates.Y < 0 || startingCoordinates.Y > Height - 1)
                throw new ApplicationException("Starting Y coordinates not valid");

            if (alignment == ShipAlignment.Horizontal && startingCoordinates.X + length-1 > Width - 1)
                throw new ApplicationException("Length is out of board size");

            if (alignment == ShipAlignment.Vertical && startingCoordinates.Y + length-1 > Height - 1)
                throw new ApplicationException("Length is out of board size");

            if (length <= 0)
                throw new ApplicationException("Length must be greater than zero");

            // Check if cell is empty
            Cell cell = null;
            for (int i=0; i<length; i++)
            {
                if (alignment == ShipAlignment.Horizontal)
                    cell = Cells.First(c => c.CellCoordinates.X == startingCoordinates.X + i && c.CellCoordinates.Y == startingCoordinates.Y);
                else
                    cell = Cells.First(c => c.CellCoordinates.X == startingCoordinates.X && c.CellCoordinates.Y == startingCoordinates.Y + i);

                if (cell.State != CellState.Empty)
                    throw new ApplicationException("Overlaps with another ship");
            }

        }

        private void ValidateBoard()
        {
            // Check if board is setup yet
            if (Cells == null || !Cells.Any() || Width == 0 || Height == 0)
                throw new ApplicationException("Board does not exists yet");
        }
    }

    public enum CellState
    {
        Empty,
        Occupied,
    }

    public enum ShipState
    {
        Alive,
        Sunk
    }

    public enum ShipAlignment
    {
        Horizontal,
        Vertical
    }

    public enum AttackResult
    {
        Hit,
        Miss
    }
}
