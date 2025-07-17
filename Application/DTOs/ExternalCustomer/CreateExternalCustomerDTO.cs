using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ExternalCustomer
{
    public class CreateExternalCustomerDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Guid? CreatedByProfessionalId { get; set; }
        public CreateExternalCustomerDTO() { }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ValidationException("Name is required");
            }

            if (string.IsNullOrWhiteSpace(Phone))
            {
                throw new ValidationException("Phone is required");
            }

        }
    }
}
