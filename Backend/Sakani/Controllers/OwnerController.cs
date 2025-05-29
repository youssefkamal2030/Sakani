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
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOwners([FromQuery] int? skip = null, [FromQuery] int? take = null)
        {
            var owners = await _ownerService.GetAllOwnersAsync(skip, take);
            return Ok(owners);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> GetOwnerById(Guid id)
        {
            var owner = await _ownerService.GetOwnerByIdAsync(id);
            if (owner == null)
                return NotFound();

            return Ok(owner);
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> GetOwnerByUserId(string userId)
        {
            var owner = await _ownerService.GetOwnerByUserIdAsync(userId);
            if (owner == null)
                return NotFound();

            return Ok(owner);
        }

        [HttpGet("verification-status/{status}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOwnersByVerificationStatus(string status, [FromQuery] int? skip = null, [FromQuery] int? take = null)
        {
            var owners = await _ownerService.GetOwnersByVerificationStatusAsync(status, skip, take);
            return Ok(owners);
        }

        [HttpGet("pending-verification")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPendingVerificationOwners([FromQuery] int? skip = null, [FromQuery] int? take = null)
        {
            var owners = await _ownerService.GetPendingVerificationOwnersAsync(skip, take);
            return Ok(owners);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateOwner([FromBody] CreateOwnerDto request)
        {
            var owner = await _ownerService.CreateOwnerAsync(request);
            return CreatedAtAction(nameof(GetOwnerById), new { id = owner.OwnerId }, owner);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> UpdateOwner(Guid id, [FromBody] UpdateOwnerDto request)
        {
            var owner = await _ownerService.UpdateOwnerAsync(id, request);
            if (owner == null)
                return NotFound();

            return Ok(owner);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOwner(Guid id)
        {
            var result = await _ownerService.DeleteOwnerAsync(id);
            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpPut("{id}/verification-status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateVerificationStatus(Guid id, [FromBody] string status)
        {
            var result = await _ownerService.UpdateVerificationStatusAsync(id, status);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{id}/apartments")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> GetOwnerWithApartments(Guid id)
        {
            var owner = await _ownerService.GetOwnerWithApartmentsAsync(id);
            if (owner == null)
                return NotFound();

            return Ok(owner);
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchOwners([FromQuery] string searchTerm, [FromQuery] int? skip = null, [FromQuery] int? take = null)
        {
            var owners = await _ownerService.SearchOwnersAsync(searchTerm, skip, take);
            return Ok(owners);
        }

        [HttpGet("count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTotalOwnersCount()
        {
            var count = await _ownerService.GetTotalOwnersCountAsync();
            return Ok(count);
        }
    }
}
