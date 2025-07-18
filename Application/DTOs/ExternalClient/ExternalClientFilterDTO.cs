using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ExternalClient
{
    public class ExternalClientFilterDto
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Guid? ProfessionalId { get; set; }
    }

}
