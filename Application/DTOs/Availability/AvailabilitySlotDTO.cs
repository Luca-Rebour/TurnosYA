using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Availibility
{
    public class AvailabilitySlotDTO
    {
        public Guid Id { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public AvailabilityStatus SlotStatus { get; set; }
        public Guid ProfessionalId { get; set; }
    }
}
