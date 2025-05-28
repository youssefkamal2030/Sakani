using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sakani.DA.DTOs;
using Sakni.Sevices.Interfaces;

namespace Sakani.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        //public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookings(
        //    [FromQuery] int? skip = null,
        //    [FromQuery] int? take = null)
        //{
        //    var bookings = await _bookingService.GetAllBookingsAsync(skip, take);
        //    return Ok(bookings);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBookingById(Guid id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetStudentBookings(
            Guid studentId,
            [FromQuery] int? skip = null,
            [FromQuery] int? take = null)
        {
            var bookings = await _bookingService.GetBookingsByStudentIdAsync(studentId, skip, take);
            return Ok(bookings);
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> CreateBooking([FromBody] CreateBookingDto request)
        {
            var booking = await _bookingService.CreateBookingAsync(request);
            return CreatedAtAction(nameof(GetBookingById), new { id = booking.BookingId }, booking);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookingDto>> UpdateBooking(Guid id, [FromBody] UpdateBookingDto request)
        {
            var booking = await _bookingService.UpdateBookingAsync(id, request);
            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> CancelBooking(Guid id)
        {
            var result = await _bookingService.CancelBookingAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("apartment/{apartmentId}")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetApartmentBookings(
            Guid apartmentId,
            [FromQuery] int? skip = null,
            [FromQuery] int? take = null)
        {
            var bookings = await _bookingService.GetBookingsByApartmentIdAsync(apartmentId, skip, take);
            return Ok(bookings);
        }

        //[HttpGet("status/{status}")]
        //public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookingsByStatus(
        //    string status,
        //    [FromQuery] int? skip = null,
        //    [FromQuery] int? take = null)
        //{
        //    var bookings = await _bookingService.get(status, skip, take);
        //    return Ok(bookings);
        //}
    }
} 