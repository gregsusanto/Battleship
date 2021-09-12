using Battleship.Controllers;
using Battleship.Models;
using Battleship.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Battleship.Controllers.BoardController;

namespace BattleshipTest
{
    [TestClass]
    public class BoardControllerTest
    {
        [TestMethod]
        public void CreateBoardTest()
        {
            // Arrange
            var mockService = new Mock<IBoardService>();
            mockService.Setup(x => x.CreateBoard()).Returns(new Board());

            // Act
            var controller = new BoardController(mockService.Object);
            var result = controller.CreateBoard();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var expectedValue = JsonSerializer.Serialize(new { Width = 10, Height = 10 }); 
            Assert.AreEqual(expectedValue, JsonSerializer.Serialize((result as OkObjectResult).Value));
        }

        [TestMethod]
        public void AddShipTest()
        {
            // Arrange
            var mockService = new Mock<IBoardService>();
            mockService.Setup(x => x.CreateBoard()).Returns(new Board());
            mockService.Setup(x => x.AddShip(It.IsAny<Coordinates>(), It.IsAny<int>(), It.IsAny<ShipAlignment>())).Returns(new Ship(0));

            var controller = new BoardController(mockService.Object);
            controller.CreateBoard();

            // Act
            var result = controller.AddShip(new BoardController.AddShipRequest { Coordinates = new Coordinates(2, 3), Length = 5, Alignment = ShipAlignment.Horizontal });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var expectedValue = JsonSerializer.Serialize(new { State = ShipState.Alive, ShipNumber = 0 });
            Assert.AreEqual(expectedValue, JsonSerializer.Serialize((result as OkObjectResult).Value));
            
            // Act
            result = controller.AddShip(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void AttackTest()
        {
            // Arrange
            var mockService = new Mock<IBoardService>();
            mockService.Setup(x => x.CreateBoard()).Returns(new Board());
            mockService.Setup(x => x.Attack(It.IsAny<Coordinates>())).Returns(AttackResult.Hit);

            var controller = new BoardController(mockService.Object);
            controller.CreateBoard();

            // Act
            var result = controller.Attack(new Coordinates(2, 3));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(AttackResult.Hit.ToString(), (result as OkObjectResult).Value);
        }
    }
}
