using JWTRegistationApp.Models;
using JWTRegistationApp.Services;
using JWTRegistationApp.Models;
using JWTRegistationApp.Services;
using Microsoft.AspNetCore.Mvc;


namespace JWTRegistationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _authService.UserExists(model.Username))
                return BadRequest("Username already exists");

            var result = await _authService.RegisterUser(model);

            if (result)
                return Ok("User registered successfully");

            return BadRequest("Something went wrong");


        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _authService.Authenticate(model);

            if (string.IsNullOrEmpty(token))
                return Unauthorized("Invalid username or password");

            return Ok(new { Token = token });
        }
    }
}
