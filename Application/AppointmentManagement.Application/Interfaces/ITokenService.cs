using AppointmentManagement.Core.Entities;
using AppointmentManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Application.Interfaces
{
    public interface ITokenService
    {
        TokenResponse GenerateToken(User user);
    }
}
