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
    public class AppointmentRepository : IAppointmentRepository
	{
		private readonly AppDbContext _context;

		public AppointmentRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
		{
			return await _context.Appointments
				.Include(a => a.Doctor) // Include Doctor details
				.ToListAsync();
		}

		public async Task<Appointment> GetAppointmentByIdAsync(int id)
		{
			return await _context.Appointments
				.Include(a => a.Doctor) // Include Doctor details
				.FirstOrDefaultAsync(a => a.AppointmentId == id);
		}

		public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
		{
			await _context.Appointments.AddAsync(appointment);
			await _context.SaveChangesAsync();
			return appointment;
		}

		public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
		{
			var existingAppointment = await _context.Appointments.FindAsync(appointment.AppointmentId);
			if (existingAppointment == null)
			{
				return null; // Return null if not found
			}

			// Update fields
			existingAppointment.PatientName = appointment.PatientName;
			existingAppointment.PatientContactInfo = appointment.PatientContactInfo;
			existingAppointment.AppointmentDateTime = appointment.AppointmentDateTime;
			existingAppointment.DoctorId = appointment.DoctorId;

			_context.Appointments.Update(existingAppointment);
			await _context.SaveChangesAsync();
			return existingAppointment;
		}

		public async Task<bool> DeleteAppointmentAsync(int id)
		{
			var appointment = await _context.Appointments.FindAsync(id);
			if (appointment == null)
			{
				return false; // Return false if not found
			}

			_context.Appointments.Remove(appointment);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
