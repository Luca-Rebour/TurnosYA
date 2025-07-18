using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int Weeks { get; set; }
        public DateTime StartDate { get; set; }
        public void Validate()
        {
            if (ProfessionalId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("User can not create an availability slot");
            }

            if (StartTime == EndTime)
            {
                throw new ValidationException("Start time and end time are required");
            }

            if (StartTime > EndTime)
            {
                throw new ValidationException("Start time must be earlier than end time");
            }
        }


    }
}
