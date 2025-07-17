using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ExternalCustomer
{
    public class ExternalCustomerDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Guid? CreatedByProfessionalId { get; set; }
        public ExternalCustomerDTO() { }
        public ExternalCustomerDTO(string name, string? email, string? phone, Guid createdByProfessionalId)
        {
            Name = name;
            Email = email;
            Phone = phone;
            CreatedByProfessionalId = createdByProfessionalId;
        }
    }
}
