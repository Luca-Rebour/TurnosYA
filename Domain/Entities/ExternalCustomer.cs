using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ExternalCustomer
    {
        public Guid Id {  get; private set; }
        public string Name { get; private set; }
        public string? Email { get; private set; }
        public string? Phone { get; private set; }
        public Guid CreatedByProfessionalId { get; private set; }

        public Professional CreatedByProfessional { get; private set; }
        
        public ExternalCustomer() { }

        public ExternalCustomer(Guid id, string name, string? email, string? phone, Guid createdByProfessionalId)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            CreatedByProfessionalId = createdByProfessionalId;
        }
    }
}
