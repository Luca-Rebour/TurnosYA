using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.Appointments;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Appointments
{
    public class CancelExpiredPendingAppointments: ICancelExpiredPendingAppointments
    {
        private IAppointmentRepository _repository;
        public CancelExpiredPendingAppointments(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Guid professionalId)
        {
            DateTime now = DateTime.UtcNow;

            IEnumerable<Appointment> pendingAppointments = await _repository.GetPendingAppointmentsByUserIdAsync(professionalId);

            IEnumerable<Appointment> toCancel = pendingAppointments
                .Where(a => a.Date.ToUniversalTime() < now)
                .ToList();

            if (toCancel.Any())
            {
                foreach (Appointment appointment in toCancel)
                {
                    appointment.SetStatusCancelled();
                }

                await _repository.SaveChangesAsync();
            }
        }
    }
}
