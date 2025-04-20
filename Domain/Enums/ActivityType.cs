using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum ActivityType
    {
        AppointmentCreated = 0,
        AppointmentCancelled = 1,
        AppointmentRescheduled = 2,
        AppointmentConfirmed = 3 
    }
}
