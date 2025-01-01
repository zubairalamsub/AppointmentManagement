using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppointmentManagement.Core.Entities
{
	public class Appointment
	{
		public int AppointmentId { get; set; }
		public string PatientName { get; set; }
		public string PatientContactInfo { get; set; }
		public DateTime AppointmentDateTime { get; set; }
		public int DoctorId { get; set; }
		[JsonIgnore]
		public Doctor Doctor { get; set; }  // Navigation property
	}
}
