﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Core.Entities
{
	public class Doctor
	{
		public int DoctorId { get; set; }
		public string DoctorName { get; set; }
		public ICollection<Appointment> Appointments { get; set; }
	}
}
