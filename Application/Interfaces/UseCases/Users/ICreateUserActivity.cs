using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Users
{
    public interface ICreateUserActivity
    {
        Task ExecuteAsync(Guid appointmentId, Guid customerId, Guid professionalId, ActivityType type, string description);
    }
}
