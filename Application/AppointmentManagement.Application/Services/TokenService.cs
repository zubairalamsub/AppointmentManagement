﻿using AppointmentManagement.Application.Interfaces;
using AppointmentManagement.Core.Entities;
using AppointmentManagement.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppointmentManagement.Application.Services
{
    public class TokenService : ITokenService
	{
		private readonly JwtSettings _jwtSettings;

		public TokenService(IOptions<JwtSettings> jwtSettings)
		{
			_jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
		}

		public TokenResponse GenerateToken(User user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));

			// Create a symmetric security key
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			// Define claims for the token
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Username),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Name, user.Username)
			};

			// Token expiration
			var tokenExpiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes);

			// Generate the JWT token
			var tokenDescriptor = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				claims: claims,
				expires: tokenExpiration,
				signingCredentials: credentials
			);

			var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

			// Return the token response
			return new TokenResponse
			{
				Token = token,
				TokenType = "Bearer",
				ExpiresIn = tokenExpiration
			};
		}
	}
}
