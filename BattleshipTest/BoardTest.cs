using Battleship.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BattleshipTest
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void CreateBoardTest()
        {
            var board = new Board();

            Assert.IsNotNull(board);
            Assert.IsTrue(board.Width > 0);
            Assert.IsTrue(board.Height > 0);
        }

        [TestMethod]
        public void CreateBoardCustomSizeTest()
        {
            var board = new Board(23, 11);

            Assert.IsNotNull(board);
            Assert.IsTrue(board.Width == 23);
            Assert.IsTrue(board.Height == 11);

            board = new Board(9999, 1234);

            Assert.IsNotNull(board);
            Assert.IsTrue(board.Width == 9999);
            Assert.IsTrue(board.Height == 1234);
        }

        [TestMethod]
        public void CreateBoardCustomSizeErrorTest()
        {
            Board board = null;

            Assert.ThrowsException<ApplicationException>(() => { board = new Board(0, 10); });
            Assert.ThrowsException<ApplicationException>(() => { board = new Board(-1, 10); });
            Assert.ThrowsException<ApplicationException>(() => { board = new Board(10, -99); });
            Assert.ThrowsException<ApplicationException>(() => { board = new Board(-1, -9); });

            Assert.IsNull(board);
        }

        [TestMethod]
        public void AddShipSuccessTest()
        {
            var board = new Board();

            var ship = board.AddShip(new Coordinates(1, 2), 3, ShipAlignment.Horizontal);
            Assert.IsNotNull(ship);
            Assert.IsTrue(ship.State == ShipState.Alive);
            Assert.IsTrue(ship.ShipNumber == 0);

            var ship2 = board.AddShip(new Coordinates(4, 2), 6, ShipAlignment.Horizontal);
            Assert.IsNotNull(ship2);
            Assert.IsTrue(ship2.State == ShipState.Alive);
            Assert.IsTrue(ship2.ShipNumber == 1);

        }

        [TestMethod]
        public void AddShipWrongInputTest()
        {
            var board = new Board();
            Ship ship = null;

            Assert.ThrowsException<ApplicationException>(() => { ship = board.AddShip(new Coordinates(11, 2), 3, ShipAlignment.Horizontal); });
            Assert.ThrowsException<ApplicationException>(() => { ship = board.AddShip(new Coordinates(2, -1), 3, ShipAlignment.Horizontal); });
            Assert.ThrowsException<ApplicationException>(() => { ship = board.AddShip(new Coordinates(10, 10), 0, ShipAlignment.Horizontal); });
            Assert.ThrowsException<ApplicationException>(() => { ship = board.AddShip(new Coordinates(10, 10), 1, ShipAlignment.Horizontal); });
            Assert.ThrowsException<ApplicationException>(() => { ship = board.AddShip(new Coordinates(0, 9), 2, ShipAlignment.Vertical); });

            Assert.IsNull(ship);
        }

        [TestMethod]
        public void AddShipOverlapTest()
        {
            var board = new Board();
            board.AddShip(new Coordinates(1, 2), 3, ShipAlignment.Horizontal);
            Ship ship = null;
            Assert.ThrowsException<ApplicationException>(() => { ship = board.AddShip(new Coordinates(1, 2), 3, ShipAlignment.Horizontal); });
            Assert.ThrowsException<ApplicationException>(() => { ship = board.AddShip(new Coordinates(3, 2), 5, ShipAlignment.Vertical); });

            Assert.IsNull(ship);
        }

        [TestMethod]
        public void AttackTest()
        {
            var board = new Board();
            board.AddShip(new Coordinates(1, 2), 3, ShipAlignment.Horizontal);

            Assert.IsTrue(board.Attack(new Coordinates(0, 2)) == AttackResult.Miss);
            Assert.IsTrue(board.Attack(new Coordinates(1, 2)) == AttackResult.Hit);
            Assert.IsTrue(board.Attack(new Coordinates(2, 2)) == AttackResult.Hit);
            Assert.IsTrue(board.Attack(new Coordinates(3, 2)) == AttackResult.Hit);
            Assert.IsTrue(board.Attack(new Coordinates(4, 2)) == AttackResult.Miss);

            Assert.IsTrue(board.Attack(new Coordinates(2, 0)) == AttackResult.Miss);
            Assert.IsTrue(board.Attack(new Coordinates(2, 1)) == AttackResult.Miss);
            Assert.IsTrue(board.Attack(new Coordinates(2, 3)) == AttackResult.Miss);
            Assert.IsTrue(board.Attack(new Coordinates(2, 4)) == AttackResult.Miss);

            Assert.IsTrue(board.Attack(new Coordinates(8, 3)) == AttackResult.Miss);

            Assert.ThrowsException<ApplicationException>(() => { board.Attack(new Coordinates(10, 3)); });
            Assert.ThrowsException<ApplicationException>(() => { board.Attack(new Coordinates(9, 11)); });
            Assert.ThrowsException<ApplicationException>(() => { board.Attack(new Coordinates(-1, 2)); });
            Assert.ThrowsException<ApplicationException>(() => { board.Attack(new Coordinates(3, -2)); });
        }
    }
}
