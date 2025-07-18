using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Client : User, IUpdatable<Client>
    {
        public void Update(Client other)
        {
            UpdateUser(other);

        }
    }
}
