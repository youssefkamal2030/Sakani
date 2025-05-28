using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sakani.DA.DTOs;
using Sakni.Sevices.Interfaces;

namespace Sakani.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        //public async Task<ActionResult<IEnumerable<FeedbackDto>>> GetAllFeedbacks(
        //    [FromQuery] int? skip = null,
        //    [FromQuery] int? take = null)
        //{
        //    var feedbacks = await _feedbackService.GetAllFeedbacksAsync(skip, take);
        //    return Ok(feedbacks);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackDto>> GetFeedbackById(Guid id)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
            if (feedback == null)
                return NotFound();

            return Ok(feedback);
        }

        [HttpGet("apartment/{apartmentId}")]
        public async Task<ActionResult<IEnumerable<FeedbackDto>>> GetApartmentFeedbacks(
            Guid apartmentId,
            [FromQuery] int? skip = null,
            [FromQuery] int? take = null)
        {
            var feedbacks = await _feedbackService.GetFeedbacksByApartmentIdAsync(apartmentId, skip, take);
            return Ok(feedbacks);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<FeedbackDto>> CreateFeedback([FromBody] CreateFeedbackDto request)
        {
            var feedback = await _feedbackService.CreateFeedbackAsync(request);
            return CreatedAtAction(nameof(GetFeedbackById), new { id = feedback.FeedbackId }, feedback);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<FeedbackDto>> UpdateFeedback(Guid id, [FromBody] UpdateFeedbackDto request)
        {
            var feedback = await _feedbackService.UpdateFeedbackAsync(id, request);
            if (feedback == null)
                return NotFound();

            return Ok(feedback);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFeedback(Guid id)
        {
            var result = await _feedbackService.DeleteFeedbackAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<FeedbackDto>>> GetUserFeedbacks(
            string userId,
            [FromQuery] int? skip = null,
            [FromQuery] int? take = null)
        {
            var feedbacks = await _feedbackService.GetFeedbacksByUserIdAsync(userId, skip, take);
            return Ok(feedbacks);
        }
    }
} 