using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Application.DTOs
{
	public class CreateAppointmentRequest
	{
		public string PatientName { get; set; }
		public string PatientContactInfo { get; set; }
		public DateTime AppointmentDateTime { get; set; }
		public int DoctorId { get; set; }
	}
}
