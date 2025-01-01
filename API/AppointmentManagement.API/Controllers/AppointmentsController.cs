using AppointmentManagement.Application.DTOs;
using AppointmentManagement.Application.Interfaces;
using AppointmentManagement.Core.Entities;
using AppointmentManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagement.API.Controllers
{
    [ApiController]
	[Route("api/[controller]")]
	public class AppointmentController : ControllerBase
	{
		private readonly IAppointmentService _appointmentService;

		public AppointmentController(IAppointmentService appointmentService)
		{
			_appointmentService = appointmentService;
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetAllAppointments()
		{
			var appointments = await _appointmentService.GetAllAppointmentsAsync();
			return Ok(appointments);
		}

		[HttpGet("{id}")]
		[Authorize]
		public async Task<IActionResult> GetAppointmentById(int id)
		{
			var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
			if (appointment == null)
			{
				return NotFound(new { Message = "Appointment not found." });
			}

			return Ok(appointment);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var createdAppointment = await _appointmentService.CreateAppointmentAsync(request);
			return CreatedAtAction(nameof(GetAppointmentById), new { id = createdAppointment.AppointmentId }, createdAppointment);
		}

		[HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var updatedAppointment = await _appointmentService.UpdateAppointmentAsync(id, request);
			if (updatedAppointment == null)
			{
				return NotFound(new { Message = "Appointment not found." });
			}

			return Ok(updatedAppointment);
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> DeleteAppointment(int id)
		{
			var deleted = await _appointmentService.DeleteAppointmentAsync(id);
			if (!deleted)
			{
				return NotFound(new { Message = "Appointment not found." });
			}

			return NoContent();
		}
	}

}
