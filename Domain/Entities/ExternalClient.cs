using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ExternalClient
    {
        public Guid Id {  get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string? Email { get; private set; }
        public string? Phone { get; private set; }
        public Guid CreatedByProfessionalId { get; private set; }
        public ICollection<ExternalAppointment> ExternalAppointments { get; private set; } = new List<ExternalAppointment>();

        public Professional CreatedByProfessional { get; private set; }
        
        public ExternalClient() { }

        public ExternalClient(string name, string? email, string? phone, Guid createdByProfessionalId)
        {
            Name = name;
            Email = email;
            Phone = phone;
            CreatedByProfessionalId = createdByProfessionalId;
        }
    }
}
