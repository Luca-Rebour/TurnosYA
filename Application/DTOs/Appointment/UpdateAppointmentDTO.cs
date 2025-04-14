using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Appointment
{
    public class UpdateAppointmentDTO
    {
        public Status Status { get; set; }
        public DateTime? Date { get; set; }
    }
}
