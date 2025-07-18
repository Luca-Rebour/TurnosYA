using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InternalAppointment: Appointment
    {
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public Guid ProfessionalId { get; set; }
        public Professional Professional { get; set; }


        public InternalAppointment(): base()
        { 
            
        }

        public void SetInternalClient(Client client)
        {
            ClientId = client.Id;
            Client = client;
        }

        public void SetProfessional(Professional professional)
        {
            Professional = professional;
        }
    }
}
