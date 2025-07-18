using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Appointment
{
    public class CreateAppointmentDTO
    {
        public Guid ClientId { get; set; }
        public Guid ProfessionalId { get; set; }
        public DateTime Date { get; set; }
        public int DurationMinutes { get; set; }
    }

}
