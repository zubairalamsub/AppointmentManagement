using AppointmentManagement.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagement.Application.Validators
{
	public class CreateAppointmentRequestValidator : AbstractValidator<CreateAppointmentRequest>
	{
		public CreateAppointmentRequestValidator()
		{
			RuleFor(x => x.PatientName)
				.NotEmpty().WithMessage("PatientName is required");

			RuleFor(x => x.AppointmentDateTime)
				.GreaterThan(DateTime.Now).WithMessage("Appointment date must be in the future");

			RuleFor(x => x.DoctorId)
				.NotEmpty().WithMessage("DoctorId is required");

			RuleFor(x => x.PatientContactInfo)
					.NotEmpty().WithMessage("PatientContactInfo is required")
			        .MaximumLength(100).WithMessage("PatientContactInfo cannot exceed 500 characters");

		}
	}
}
