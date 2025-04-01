using Microsoft.AspNetCore.Mvc;
using Sakani.Contracts.Dto;
using Sakani.Services.Interfaces;
namespace Sakani.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        
        private readonly IAuthService _authService;
        private readonly ILogger _logger;
        public AuthController(IAuthService authService, ILogger logger)
        {
            _authService = authService;
            _logger = logger;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var (success, errorMessage) = await _authService.RegisterUserAsync(registerDto);

                if (success)
                {
                    return Ok(new { Message = "User registered successfully" });
                }
                else
                {
                    return BadRequest(new { Error = errorMessage });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Internal Server Error"});
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var (token, errorMessage) = await _authService.LoginUserAsync(loginDto);

                if (token != null)
                {
                    return Ok(new { Token = token });
                }
                else
                {
                    return BadRequest(new { Error = errorMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in login");
                return StatusCode(500, new { Error = "Internal Server Error"});
            }

        }
    }
}
