using AppointmentManagement.Application.DTOs;
using AppointmentManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagement.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
		{
			if (registerRequest == null)
			{
				return BadRequest("Invalid request");
			}

			var result = await _userService.RegisterUserAsync(registerRequest);

			if (result == "Username already exists")
			{
				return Conflict(result);
			}

			return Ok(result);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
		{
			if (loginRequest == null)
			{
				return BadRequest("Invalid request");
			}

			var token = await _userService.LoginUserAsync(loginRequest);

			if (token == null)
			{
				return Unauthorized("Invalid credentials");
			}

			return Ok(token);
		}
	}
}
