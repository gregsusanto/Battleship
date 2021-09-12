using Battleship.Models;
using Battleship.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;

        public class AddShipRequest
        {
            public Coordinates Coordinates { get; set; }
            public int Length { get; set; }
            public ShipAlignment Alignment { get; set; }
        }

        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpPost]
        public IActionResult CreateBoard()
        {
            try
            {
                var board = _boardService.CreateBoard();
                return Ok(new { board.Width, board.Height });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("Ship")]
        public IActionResult AddShip([FromBody]AddShipRequest addShipRequest)
        {
            if (addShipRequest == null || addShipRequest.Coordinates == null)
                return BadRequest("input not valid");

            try
            {
                var ship = _boardService.AddShip(addShipRequest.Coordinates, addShipRequest.Length, addShipRequest.Alignment);

                return Ok(ship);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Attack")]
        public IActionResult Attack([FromBody]Coordinates coordinates)
        {
            if (coordinates == null)
                return BadRequest("no input");

            try
            {
                var attackResult = _boardService.Attack(coordinates);

                return Ok(attackResult.ToString());
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
