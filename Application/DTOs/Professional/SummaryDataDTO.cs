using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Professional
{
    public class SummaryDataDTO
    {
        public int TodayAppointments { get; set; }
        public int ThisWeekAppointments { get; set; }
        public int Activeclients { get; set; }
        public int PendingConfirmations { get; set; }

        public SummaryDataDTO(int todayAppointments, int thisWeekAppointments, int activeclients, int pendingConfirmations)
        {
            TodayAppointments = todayAppointments;
            ThisWeekAppointments = thisWeekAppointments;
            Activeclients = activeclients;
            PendingConfirmations = pendingConfirmations;
        }
    }
}
