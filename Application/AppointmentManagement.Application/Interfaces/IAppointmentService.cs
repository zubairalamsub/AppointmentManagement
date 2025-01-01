using AppointmentManagement.Application.DTOs;
using AppointmentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(int id);
        Task<Appointment> CreateAppointmentAsync(CreateAppointmentRequest request);
        Task<Appointment> UpdateAppointmentAsync(int id, UpdateAppointmentRequest request);
        Task<bool> DeleteAppointmentAsync(int id);
    }

}
