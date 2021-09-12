using Battleship.Models;
using Battleship.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipTest
{
    [TestClass]
    public class BoardServiceTest
    {
        [TestMethod]
        public void CreateBoardTest()
        {
            // Arrange
            var service = new BoardService();

            // Act
            var board = service.CreateBoard();

            // Assert
            Assert.IsNotNull(board);
            Assert.AreEqual(10, board.Width);
            Assert.AreEqual(10, board.Height);
        }

        [TestMethod]
        public void AddShipTest()
        {
            // Arrange
            var service = new BoardService();
            Assert.ThrowsException<ApplicationException>(() => { service.AddShip(new Coordinates(0, 0), 5, ShipAlignment.Vertical); });
            var board = service.CreateBoard();

            // Act
            var ship = service.AddShip(new Coordinates(0, 0), 5, ShipAlignment.Vertical);

            // Assert
            Assert.IsNotNull(ship);
            Assert.AreEqual(ShipState.Alive, ship.State);
            Assert.AreEqual(0, ship.ShipNumber);

            // Act
            var ship2 = service.AddShip(new Coordinates(2, 0), 3, ShipAlignment.Vertical);

            // Assert
            Assert.IsNotNull(ship2);
            Assert.AreEqual(ShipState.Alive, ship2.State);
            Assert.AreEqual(1, ship2.ShipNumber);
        }

        [TestMethod]
        public void AttackTest()
        {
            // Arrange
            var service = new BoardService();
            Assert.ThrowsException<ApplicationException>(() => { service.Attack(new Coordinates(0, 0)); });
            var board = service.CreateBoard();
            service.AddShip(new Coordinates(0, 0), 5, ShipAlignment.Horizontal);

            // Act and Assert
            Assert.AreEqual(AttackResult.Hit, service.Attack(new Coordinates(0, 0)));
            Assert.AreEqual(AttackResult.Hit, service.Attack(new Coordinates(1, 0)));
            Assert.AreEqual(AttackResult.Hit, service.Attack(new Coordinates(2, 0)));
            Assert.AreEqual(AttackResult.Hit, service.Attack(new Coordinates(3, 0)));
            Assert.AreEqual(AttackResult.Hit, service.Attack(new Coordinates(4, 0)));
            Assert.AreEqual(AttackResult.Miss, service.Attack(new Coordinates(5, 0)));

            Assert.AreEqual(AttackResult.Miss, service.Attack(new Coordinates(0, 1)));
            Assert.AreEqual(AttackResult.Miss, service.Attack(new Coordinates(0, 2)));
            Assert.AreEqual(AttackResult.Miss, service.Attack(new Coordinates(0, 3)));
            Assert.AreEqual(AttackResult.Miss, service.Attack(new Coordinates(0, 4)));
            Assert.AreEqual(AttackResult.Miss, service.Attack(new Coordinates(0, 5)));
        }
    }
}
