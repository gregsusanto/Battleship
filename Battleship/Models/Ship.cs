namespace Battleship.Models
{
    public class Ship
    {
        public Ship(int shipNumber)
        {
            ShipNumber = shipNumber;
            State = ShipState.Alive;
        }

        public ShipState State { get; set; }
        public int ShipNumber { get; private set; }
    }
}
