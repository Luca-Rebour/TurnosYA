using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ExternalAppointment: Appointment
    {
        public Guid ExternalClientId { get; set; }
        public ExternalClient ExternalClient { get; set; }
        public Guid ProfessionalId { get; set; }
        public Professional Professional { get; set; }

        public ExternalAppointment(): base ()
        {

        }

        public void SetExternalClient(ExternalClient externalClient)
        {
            ExternalClientId = externalClient.Id;
            ExternalClient = externalClient;

        }
        public void SetProfessional(Professional professional)
        {
            Professional = professional;
        }

    }
}
