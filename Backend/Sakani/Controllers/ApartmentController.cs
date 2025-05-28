using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sakani.DA.DTOs;
using Sakni.Sevices.Interfaces;

namespace Sakani.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;

        public ApartmentController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApartmentDto>>> GetAllApartments(
            [FromQuery] int? skip = null,
            [FromQuery] int? take = null)
        {
            var apartments = await _apartmentService.GetAllApartmentsAsync(skip, take);
            return Ok(apartments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApartmentDto>> GetApartmentById(Guid id)
        {
            var apartment = await _apartmentService.GetApartmentByIdAsync(id);
            if (apartment == null)
                return NotFound();

            return Ok(apartment);
        }

        [HttpGet("owner/{ownerId}")]
        public async Task<ActionResult<IEnumerable<ApartmentDto>>> GetApartmentsByOwner(
            Guid ownerId,
            [FromQuery] int? skip = null,
            [FromQuery] int? take = null)
        {
            var apartments = await _apartmentService.GetApartmentsByOwnerIdAsync(ownerId, skip, take);
            return Ok(apartments);
        }

        [Authorize(Roles = "Owner")]
        [HttpPost]
        public async Task<ActionResult<ApartmentDto>> CreateApartment([FromBody] CreateApartmentDto request)
        {
            var apartment = await _apartmentService.CreateApartmentAsync(request);
            return CreatedAtAction(nameof(GetApartmentById), new { id = apartment.ApartmentId }, apartment);
        }

        [Authorize(Roles = "Owner")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApartmentDto>> UpdateApartment(Guid id, [FromBody] UpdateApartmentDto request)
        {
            var apartment = await _apartmentService.UpdateApartmentAsync(id, request);
            if (apartment == null)
                return NotFound();

            return Ok(apartment);
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteApartment(Guid id)
        {
            var result = await _apartmentService.DeleteApartmentAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<ApartmentDto>> GetApartmentWithDetails(Guid id)
        {
            var apartment = await _apartmentService.GetApartmentWithDetailsAsync(id);
            if (apartment == null)
                return NotFound();

            return Ok(apartment);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ApartmentDto>>> SearchApartments(
            [FromQuery] string searchTerm,
            [FromQuery] int? skip = null,
            [FromQuery] int? take = null)
        {
            var apartments = await _apartmentService.SearchApartmentsAsync(searchTerm, skip, take);
            return Ok(apartments);
        }

        [HttpGet("location/{location}")]
        public async Task<ActionResult<IEnumerable<ApartmentDto>>> GetApartmentsByLocation(
            string location,
            [FromQuery] int? skip = null,
            [FromQuery] int? take = null)
        {
            var apartments = await _apartmentService.GetApartmentsByLocationAsync(location, skip, take);
            return Ok(apartments);
        }

        [HttpGet("with-rooms")]
        public async Task<ActionResult<IEnumerable<ApartmentDto>>> GetApartmentsWithRooms(
            [FromQuery] int? skip = null,
            [FromQuery] int? take = null)
        {
            var apartments = await _apartmentService.GetApartmentsWithRoomsAsync(skip, take);
            return Ok(apartments);
        }

        [HttpGet("with-feedbacks")]
        public async Task<ActionResult<IEnumerable<ApartmentDto>>> GetApartmentsWithFeedbacks(
            [FromQuery] int? skip = null,
            [FromQuery] int? take = null)
        {
            var apartments = await _apartmentService.GetApartmentsWithFeedbacksAsync(skip, take);
            return Ok(apartments);
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetTotalApartmentsCount()
        {
            var count = await _apartmentService.GetTotalApartmentsCountAsync();
            return Ok(count);
        }
    }
} 