using AppointmentManagement.Core.Entities;
using AppointmentManagement.Infrastructure.Data;
using AppointmentManagement.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context; // Replace with your actual DbContext

		public UserRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<User> GetUserByUsernameAsync(string username)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
		}

		public async Task<User> CreateUserAsync(User user)
		{
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return user;
		}
	}
}
