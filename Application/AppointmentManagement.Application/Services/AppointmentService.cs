using AppointmentManagement.Application.DTOs;
using AppointmentManagement.Application.Interfaces;
using AppointmentManagement.Core.Entities;
using AppointmentManagement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Application.Services
{
    public class AppointmentService : IAppointmentService
	{
		private readonly IAppointmentRepository _appointmentRepository;
		private readonly IDoctorRepository _doctorRepository; 

		public AppointmentService(IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository)
		{
			_appointmentRepository = appointmentRepository;
			_doctorRepository = doctorRepository;
		}

		public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
		{
			return await _appointmentRepository.GetAllAppointmentsAsync();
		}

		public async Task<Appointment> GetAppointmentByIdAsync(int id)
		{
			return await _appointmentRepository.GetAppointmentByIdAsync(id);
		}

		public async Task<Appointment> CreateAppointmentAsync(CreateAppointmentRequest request)
		{
			// Validate Doctor ID
			var doctorExists = await _doctorRepository.DoesDoctorExistAsync(request.DoctorId);
			if (!doctorExists)
			{
				throw new ArgumentException("Invalid Doctor ID provided.");
			}

			var appointment = new Appointment
			{
				PatientName = request.PatientName,
				PatientContactInfo = request.PatientContactInfo,
				AppointmentDateTime = request.AppointmentDateTime,
				DoctorId = request.DoctorId
			};

			return await _appointmentRepository.CreateAppointmentAsync(appointment);
		}

		public async Task<Appointment> UpdateAppointmentAsync(int id, UpdateAppointmentRequest request)
		{
			var existingAppointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
			if (existingAppointment == null)
			{
				throw new KeyNotFoundException("Appointment not found.");
			}

		
			var doctorExists = await _doctorRepository.DoesDoctorExistAsync(request.DoctorId);
			if (!doctorExists)
			{
				throw new ArgumentException("Invalid Doctor ID provided.");
			}

			
			existingAppointment.PatientName = request.PatientName;
			existingAppointment.PatientContactInfo = request.PatientContactInfo;
			existingAppointment.AppointmentDateTime = request.AppointmentDateTime;
			existingAppointment.DoctorId = request.DoctorId;

			return await _appointmentRepository.UpdateAppointmentAsync(existingAppointment);
		}

		public async Task<bool> DeleteAppointmentAsync(int id)
		{
			var existingAppointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
			if (existingAppointment == null)
			{
				throw new KeyNotFoundException("Appointment not found.");
			}

			return await _appointmentRepository.DeleteAppointmentAsync(id);
		}
	}
}
