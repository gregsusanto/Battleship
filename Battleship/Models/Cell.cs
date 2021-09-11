namespace Battleship.Models
{
    public class Cell
    {
        public Cell(int x, int y)
        {
            CellCoordinates = new Coordinates(x, y);
            State = CellState.Empty;
        }

        public Coordinates CellCoordinates { get; set; }
        public CellState State { get; set; }
        public int ShipNumber { get; set; }
    }
}
