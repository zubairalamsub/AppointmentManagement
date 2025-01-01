using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Core.Models
{
	public class TokenResponse
	{
		public string Token { get; set; }
		public string TokenType { get; set; } // Typically "Bearer"
		public DateTime ExpiresIn { get; set; }    // Token expiration time in seconds
	}
}
