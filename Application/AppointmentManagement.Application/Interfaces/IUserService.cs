using AppointmentManagement.Application.DTOs;
using AppointmentManagement.Core.Entities;
using AppointmentManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Application.Interfaces
{

    public interface IUserService
    {
        Task<string> RegisterUserAsync(RegisterRequest registerRequest);
        Task<TokenResponse> LoginUserAsync(LoginRequest loginRequest);
    }

}
