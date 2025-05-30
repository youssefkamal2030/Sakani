using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sakani.DA.DTOs;
using Sakni.Sevices.Interfaces;

namespace Sakani.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("apartment/{apartmentId}")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRoomsByApartmentId(
            Guid apartmentId,
            [FromQuery] int? skip = null,
            [FromQuery] int? take = null)
        {
            try
            {
                var rooms = await _roomService.GetRoomsByApartmentIdAsync(apartmentId, skip, take);
                if (rooms == null || !rooms.Any())
                    return NotFound("No rooms found for this apartment");

                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetRoomById(Guid id)
        {
            try
            {
                var room = await _roomService.GetRoomByIdAsync(id);
                if (room == null)
                    return NotFound($"Room with ID {id} not found");

                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<RoomDto>> GetRoomWithDetails(Guid id)
        {
            try
            {
                var room = await _roomService.GetRoomWithDetailsAsync(id);
                if (room == null)
                    return NotFound($"Room with ID {id} not found");

                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/beds")]
        public async Task<ActionResult<RoomDto>> GetRoomWithBeds(Guid id)
        {
            try
            {
                var room = await _roomService.GetRoomWithBedsAsync(id);
                if (room == null)
                    return NotFound($"Room with ID {id} not found");

                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("type/{roomType}")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRoomsByType(
            string roomType,
            [FromQuery] int? skip = null,
            [FromQuery] int? take = null)
        {
            try
            {
                var rooms = await _roomService.GetRoomsByTypeAsync(roomType, skip, take);
                if (rooms == null || !rooms.Any())
                    return NotFound($"No rooms found of type {roomType}");

                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPost]
        public async Task<ActionResult<RoomDto>> CreateRoom([FromBody] CreateRoomDto request)
        {
            try
            {
                var room = await _roomService.CreateRoomAsync(request);
                return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<RoomDto>> UpdateRoom(Guid id, [FromBody] UpdateRoomDto request)
        {
            try
            {
                var room = await _roomService.UpdateRoomAsync(id, request);
                if (room == null)
                    return NotFound($"Room with ID {id} not found");

                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpPut("{id}/capacity")]
        public async Task<ActionResult> UpdateRoomCapacity(Guid id, [FromBody] int numberOfBeds)
        {
            try
            {
                var success = await _roomService.UpdateRoomCapacityAsync(id, numberOfBeds);
                if (!success)
                    return NotFound($"Room with ID {id} not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Owner, Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRoom(Guid id)
        {
            try
            {
                var success = await _roomService.DeleteRoomAsync(id);
                if (!success)
                    return NotFound($"Room with ID {id} not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetTotalRoomsCount()
        {
            try
            {
                var count = await _roomService.GetTotalRoomsCountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
