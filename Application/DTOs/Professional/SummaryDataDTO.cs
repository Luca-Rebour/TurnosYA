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
        public int ActiveClients { get; set; }
        public int PendingConfirmations { get; set; }

        public SummaryDataDTO(int todayAppointments, int thisWeekAppointments, int activeClients, int pendingConfirmations)
        {
            TodayAppointments = todayAppointments;
            ThisWeekAppointments = thisWeekAppointments;
            ActiveClients = activeClients;
            PendingConfirmations = pendingConfirmations;
        }
    }
}
