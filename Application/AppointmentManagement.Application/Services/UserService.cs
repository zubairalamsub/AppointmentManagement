using AppointmentManagement.Core.Models;
using AppointmentManagement.Core.Entities;
using AppointmentManagement.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using AppointmentManagement.Application.Interfaces;
using AppointmentManagement.Infrastructure.Interfaces;

namespace AppointmentManagement.Application.Services
{
    public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly ITokenService _tokenService;
		private readonly IPasswordHasher<User> _passwordHasher; 

		public UserService(IUserRepository userRepository, ITokenService tokenService, IPasswordHasher<User> passwordHasher)
		{
			_userRepository = userRepository;
			_tokenService = tokenService;
			_passwordHasher = passwordHasher; 
		}

		public async Task<string> RegisterUserAsync(RegisterRequest registerRequest)
		{
			var existingUser = await _userRepository.GetUserByUsernameAsync(registerRequest.Username);
			if (existingUser != null)
			{
				return "Username already exists";
			}

			// Use PasswordHasher to hash the password
			var user = new User
			{
				Username = registerRequest.Username,
				PasswordHash = _passwordHasher.HashPassword(null, registerRequest.Password) 
			};

			var createdUser = await _userRepository.CreateUserAsync(user);
			return "User registered successfully";
		}

		public async Task<TokenResponse> LoginUserAsync(LoginRequest loginRequest)
		{
			var user = await _userRepository.GetUserByUsernameAsync(loginRequest.Username);
			if (user == null)
			{
				return null;
			}

			
			bool isPasswordValid = VerifyPassword(user, loginRequest.Password);
			if (!isPasswordValid)
			{
				return null;
			}

			var token = _tokenService.GenerateToken(user);
			return token;
		}

		private bool VerifyPassword(User user, string password)
		{
			
			var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
			return result == PasswordVerificationResult.Success;
		}
	}
}
