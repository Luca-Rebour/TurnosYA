using Application.DTOs.Appointment;
using Application.DTOs.Client;
using Application.DTOs.Professional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.UserActivity
{
    public class UserActivityDTO
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid ClientId { get; set; }
        public Guid ProfessionalId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public ClientShortDTO? Client { get; set; }
        public ProfessionalShortDTO? Professional { get; set; }
        public AppointmentDTO? Appointment { get; set; }
    }

}
