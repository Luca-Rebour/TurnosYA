using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Appointment
{
    public class CreateInternalAppointmentDTO
    {
        public Guid CustomerId { get; set; }
        public Guid ProfessionalId { get; set; }
        public DateTime Date { get; set; }
        public int DurationMinutes { get; set; }

        public void Validate()
        {
            if (CustomerId == Guid.Empty)
                throw new ValidationException("Customer is required.");

            if (ProfessionalId == Guid.Empty)
                throw new UnauthorizedAccessException("User cannot create an appointment without a valid professional ID.");

            if (Date == default)
                throw new ValidationException("Appointment date is required.");

            if (Date < DateTime.UtcNow)
                throw new ValidationException("Appointment date must be in the future.");

            if (DurationMinutes <= 0)
                throw new ValidationException("Duration must be greater than zero.");

            if (DurationMinutes > 480)
                throw new ValidationException("Duration cannot exceed 480 minutes (8 hours).");
        }

    }

}
