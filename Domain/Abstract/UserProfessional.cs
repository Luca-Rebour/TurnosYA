using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public abstract class UserProfessional : User
    {
        public ICollection<AvailabilitySlot> Availability { get; private set; }
        public abstract IEnumerable<Client> GetClients();
        public abstract IEnumerable<AvailabilitySlot> GetAvailability();
        public virtual void AddAvailabilitySlot(AvailabilitySlot slot)
        {
            Availability.Add(slot);
        }
        public UserProfessional()
        {
            Availability = new List<AvailabilitySlot>();
        }
        public void UpdateUserProfessional(UserProfessional other)
        {
            if (other == null)
                return;

            UpdateUser(other);

            if (other.Availability != null && other.Availability.Any())
            {
                Availability = new List<AvailabilitySlot>(other.Availability);
            }
        }

        public void UpdateAvailability(IEnumerable<AvailabilitySlot> other)
        {
            if (other == null || !other.Any())
                return;

            Availability = new List<AvailabilitySlot>(other);
        }


    }
}
