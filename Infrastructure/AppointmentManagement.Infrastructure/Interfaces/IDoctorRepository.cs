using AppointmentManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Infrastructure.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor?> GetDoctorByIdAsync(int doctorId);
        Task<Doctor> AddDoctorAsync(string doctorName);
        Task<bool> DeleteDoctorAsync(int doctorId);
        Task<bool> DoesDoctorExistAsync(int doctorId);
    }
}
