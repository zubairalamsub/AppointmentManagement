using AppointmentManagement.Application.Interfaces;
using AppointmentManagement.Core.Entities;
using AppointmentManagement.Infrastructure.Data;
using AppointmentManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Application.Services
{
    public class DoctorService : IDoctorService
	{
		private readonly IDoctorRepository _doctorRepository;

		public DoctorService(IDoctorRepository doctorRepository)
		{
			_doctorRepository = doctorRepository;
		}

		public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
		{
			return await _doctorRepository.GetAllDoctorsAsync();
		}

		public async Task<Doctor?> GetDoctorByIdAsync(int doctorId)
		{
			return await _doctorRepository.GetDoctorByIdAsync(doctorId);
		}

		public async Task<Doctor> AddDoctorAsync(string doctorName)
		{
			return await _doctorRepository.AddDoctorAsync(doctorName);
		}

		public async Task<bool> DeleteDoctorAsync(int doctorId)
		{
			return await _doctorRepository.DeleteDoctorAsync(doctorId);
		}
	}
}
