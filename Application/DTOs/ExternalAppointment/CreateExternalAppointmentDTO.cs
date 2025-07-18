using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.DTOs.Appointment
{
    public class CreateExternalAppointmentDTO
    {
        public Guid ExternalClientId { get; set; }
        public Guid ProfessionalId { get; set; }
        public DateTime Date { get; set; }
        public int DurationMinutes { get; set; }

        public void Validate()
        {
            if (ExternalClientId == Guid.Empty)
                throw new ValidationException("External client is required.");

            if (ProfessionalId == Guid.Empty)
                throw new UnauthorizedAccessException("User cannot create an appointment without a valid professional ID.");

            if (Date == default)
                throw new ValidationException("Appointment date is required.");

            if (DurationMinutes <= 0)
                throw new ValidationException("Duration must be greater than zero.");

            if (DurationMinutes > 480) 
                throw new ValidationException("Duration cannot exceed 480 minutes (8 hours).");
        }
    }

}
