using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Availability
{
    public class CreateAvailabilitySlotDTO
    {
        public DayOfWeek DayOfWeek {  get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Guid ProfessionalId { get; set; }

    }
}
