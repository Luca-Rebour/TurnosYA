using Application.DTOs.Client;
using Application.DTOs.Professional;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Appointment
{
    public class InternalAppointmentDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int DurationMinutes { get; set; }
        public string Status { get; set; }
        public string? CancelledBy { get; set; }

        public ClientShortDTO Client { get; set; }
        public ProfessionalShortDTO Professional { get; set; }
    }


}
