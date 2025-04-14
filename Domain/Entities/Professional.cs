using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Professional : UserProfessional, IUpdatable<Professional>
    {
        public Professional():base() { }

        public override IEnumerable<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }
        public override IEnumerable<AvailabilitySlot> GetAvailability()
        {
            throw new NotImplementedException();
        }

        public void Update(Professional other)
        {
            UpdateUserProfessional(other);
        }
        

    }
}
