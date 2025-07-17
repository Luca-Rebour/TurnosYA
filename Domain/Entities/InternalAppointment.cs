using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InternalAppointment: Appointment
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Guid ProfessionalId { get; set; }
        public Professional Professional { get; set; }


        public InternalAppointment(): base()
        { 
            
        }

        public void SetCustomer(Customer customer)
        {
            CustomerId = customer.Id;
            Customer = customer;
        }

        public void SetProfessional(Professional professional)
        {
            Professional = professional;
        }
    }
}
