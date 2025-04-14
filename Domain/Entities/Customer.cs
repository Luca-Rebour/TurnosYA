using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Customer : User, IUpdatable<Customer>
    {
        public void Update(Customer other)
        {
            UpdateUser(other);

        }
    }
}
