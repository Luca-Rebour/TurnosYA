using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ExternalClients
{
    public class ExternalClientFilterDTO
    {
        public string? Email {  get; set; }
        public string? Phone { get; set; }
        public Guid? ProfessionalId { get; set; }
    }
}
