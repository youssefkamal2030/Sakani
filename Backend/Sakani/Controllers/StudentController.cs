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
   
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllStudents([FromQuery] int? skip = null, [FromQuery] int? take = null)
        {
            var students = await _studentService.GetAllStudentsAsync(skip, take);
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> GetStudentByUserId(string userId)
        {
            var student = await _studentService.GetStudentByUserIdAsync(userId);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpGet("college/{collegeName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetStudentsByCollege(string collegeName, [FromQuery] int? skip = null, [FromQuery] int? take = null)
        {
            var students = await _studentService.GetStudentsByCollegeAsync(collegeName, skip, take);
            return Ok(students);
        }

        [HttpGet("origin/{origin}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetStudentsByOrigin(string origin, [FromQuery] int? skip = null, [FromQuery] int? take = null)
        {
            var students = await _studentService.GetStudentsByOriginAsync(origin, skip, take);
            return Ok(students);
        }

        [HttpGet("with-bookings")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetStudentsWithBookings([FromQuery] int? skip = null, [FromQuery] int? take = null)
        {
            var students = await _studentService.GetStudentsWithBookingsAsync(skip, take);
            return Ok(students);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDto request)
        {
            var student = await _studentService.CreateStudentAsync(request);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.StudentId }, student);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] UpdateStudentDto request)
        {
            var student = await _studentService.UpdateStudentAsync(id, request);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var result = await _studentService.DeleteStudentAsync(id);
            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchStudents([FromQuery] string searchTerm, [FromQuery] int? skip = null, [FromQuery] int? take = null)
        {
            var students = await _studentService.SearchStudentsAsync(searchTerm, skip, take);
            return Ok(students);
        }

        [HttpGet("count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTotalStudentsCount()
        {
            var count = await _studentService.GetTotalStudentsCountAsync();
            return Ok(count);
        }
    }
} 