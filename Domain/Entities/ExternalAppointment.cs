using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ExternalAppointment: Appointment
    {
        public Guid ExternalCustomerId { get; set; }
        public ExternalCustomer ExternalCustomer { get; set; }
        public Guid ProfessionalId { get; set; }
        public Professional Professional { get; set; }

        public ExternalAppointment(): base ()
        {

        }

        public void SetCustomer(ExternalCustomer externalCustomer)
        {
            ExternalCustomerId = externalCustomer.Id;
            ExternalCustomer = externalCustomer;

        }
        public void SetProfessional(Professional professional)
        {
            Professional = professional;
        }

    }
}
