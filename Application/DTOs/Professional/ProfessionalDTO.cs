using Application.DTOs.Availibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Professional
{
    public class ProfessionalDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<AvailabilitySlotDTO> Availability { get; set; }
    }
}
