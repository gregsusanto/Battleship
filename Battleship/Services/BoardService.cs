using Battleship.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Services
{
    public class BoardService : IBoardService
    {
        private Board CurrentBoard;

        public Ship AddShip(Coordinates startingCoordinates, int length, ShipAlignment alignment)
        {
            if (CurrentBoard == null)
                throw new ApplicationException("Board not created yet");

            return CurrentBoard.AddShip(startingCoordinates, length, alignment);
        }

        public AttackResult Attack(Coordinates coordinates)
        {
            if (CurrentBoard == null)
                throw new ApplicationException("Board not created yet");

            return CurrentBoard.Attack(coordinates);
        }

        public Board CreateBoard()
        {
            var board = new Board();
            CurrentBoard = board;
            return board;
        }

    }
}
