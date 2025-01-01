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
    public class DoctorRepository : IDoctorRepository
	{
		private readonly AppDbContext _context;

		public DoctorRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
		{
			return await _context.Doctors.ToListAsync();
		}

		public async Task<Doctor?> GetDoctorByIdAsync(int doctorId)
		{
			return await _context.Doctors.FindAsync(doctorId);
		}

		public async Task<Doctor> AddDoctorAsync(string doctorName)
		{
			var doctor = new Doctor { DoctorName = doctorName };
			_context.Doctors.Add(doctor);
			await _context.SaveChangesAsync();
			return doctor;
		}

		public async Task<bool> DeleteDoctorAsync(int doctorId)
		{
			var doctor = await _context.Doctors.FindAsync(doctorId);
			if (doctor == null) return false;

			_context.Doctors.Remove(doctor);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<bool> DoesDoctorExistAsync(int doctorId)
		{
			return await _context.Doctors.AnyAsync(d => d.DoctorId == doctorId);
		}
	}
}
