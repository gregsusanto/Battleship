using Battleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Services
{
    public interface IBoardService
    {
        Board CreateBoard();
        Ship AddShip(Coordinates startingCoordinates, int length, ShipAlignment alignment);
        AttackResult Attack(Coordinates coordinates);
    }
}
