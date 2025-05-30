using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sakani.DA.DTOs;
using Sakni.Sevices.Interfaces;
using System;
using System.Threading.Tasks;

namespace Sakani.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Owner")]
    public class BedController : ControllerBase
    {
        private readonly IBedService _bedService;

        public BedController(IBedService bedService)
        {
            _bedService = bedService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> GetBedById(Guid id)
        {
            var bed = await _bedService.GetBedByIdAsync(id);
            if (bed == null)
                return NotFound();

            return Ok(bed);
        }

        [HttpGet("room/{roomId}")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> GetBedsByRoomId(Guid roomId, [FromQuery] int? skip = null, [FromQuery] int? take = null)
        {
            var beds = await _bedService.GetBedsByRoomIdAsync(roomId, skip, take);
            return Ok(beds);
        }

        [HttpGet("available")]
        [Authorize(Roles = "Admin,Owner,Student")]
        public async Task<IActionResult> GetAvailableBeds([FromQuery] int? skip = null, [FromQuery] int? take = null)
        {
            var beds = await _bedService.GetAvailableBedsAsync(skip, take);
            return Ok(beds);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> CreateBed([FromBody] CreateBedDto request)
        {
            var bed = await _bedService.CreateBedAsync(request);
            return CreatedAtAction(nameof(GetBedById), new { id = bed.Id }, bed);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> UpdateBed(Guid id, [FromBody] UpdateBedDto request)
        {
            var bed = await _bedService.UpdateBedAsync(id, request);
            if (bed == null)
                return NotFound();

            return Ok(bed);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> DeleteBed(Guid id)
        {
            var result = await _bedService.DeleteBedAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{id}/student")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> GetBedWithStudent(Guid id)
        {
            var bed = await _bedService.GetBedWithStudentAsync(id);
            if (bed == null)
                return NotFound();

            return Ok(bed);
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> UpdateBedStatus(Guid id, [FromBody] bool isVacant)
        {
            var result = await _bedService.UpdateBedStatusAsync(id, isVacant);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}/assign/{studentId}")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> AssignStudentToBed(Guid id, Guid studentId)
        {
            var result = await _bedService.AssignStudentToBedAsync(id, studentId);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}/remove-student")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> RemoveStudentFromBed(Guid id)
        {
            var result = await _bedService.RemoveStudentFromBedAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("price-range")]
        [Authorize(Roles = "Admin,Owner,Student")]
        public async Task<IActionResult> GetBedsByPriceRange(
            [FromQuery] decimal minPrice,
            [FromQuery] decimal maxPrice,
            [FromQuery] int? skip = null,
            [FromQuery] int? take = null)
        {
            var beds = await _bedService.GetBedsByPriceRangeAsync(minPrice, maxPrice, skip, take);
            return Ok(beds);
        }

        [HttpGet("{id}/details")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> GetBedWithDetails(Guid id)
        {
            var bed = await _bedService.GetBedWithDetailsAsync(id);
            if (bed == null)
                return NotFound();

            return Ok(bed);
        }

        [HttpPut("{id}/price")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> UpdateBedPrice(Guid id, [FromBody] decimal newPrice)
        {
            var result = await _bedService.UpdateBedPriceAsync(id, newPrice);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("count")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> GetTotalBedsCount()
        {
            var count = await _bedService.GetTotalBedsCountAsync();
            return Ok(count);
        }
    }
} 