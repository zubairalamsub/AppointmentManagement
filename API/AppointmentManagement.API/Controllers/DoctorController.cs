using AppointmentManagement.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagement.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class DoctorController : ControllerBase
	{
		private readonly IDoctorService _doctorService;

		public DoctorController(IDoctorService doctorService)
		{
			_doctorService = doctorService;
		}

		// GET: api/doctor
		[HttpGet]
		public async Task<IActionResult> GetDoctors()
		{
			var doctors = await _doctorService.GetAllDoctorsAsync();
			if (doctors == null || !doctors.Any())
			{
				return NotFound("No doctors found.");
			}

			return Ok(doctors);
		}

		// GET: api/doctor/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetDoctor(int id)
		{
			var doctor = await _doctorService.GetDoctorByIdAsync(id);
			if (doctor == null)
			{
				return NotFound($"Doctor with ID {id} not found.");
			}

			return Ok(doctor);
		}

		// POST: api/doctor
		[HttpPost]
		public async Task<IActionResult> AddDoctor([FromBody] string doctorName)
		{
			if (string.IsNullOrWhiteSpace(doctorName))
			{
				return BadRequest("Doctor name cannot be empty.");
			}

			var doctor = await _doctorService.AddDoctorAsync(doctorName);
			return CreatedAtAction(nameof(GetDoctor), new { id = doctor.DoctorId }, doctor);
		}

		// DELETE: api/doctor/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDoctor(int id)
		{
			var result = await _doctorService.DeleteDoctorAsync(id);
			if (!result)
			{
				return NotFound($"Doctor with ID {id} not found.");
			}

			return NoContent();  // Successful deletion, no content to return
		}
	}
}
