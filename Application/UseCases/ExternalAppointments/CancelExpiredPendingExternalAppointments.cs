using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.Appointments;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.ExternalAppointments
{
    public class CancelExpiredPendingExternalAppointments : ICancelExpiredPendingExternalAppointments
    {
        private IExternalAppointmentRepository _repository;
        public CancelExpiredPendingExternalAppointments(IExternalAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Guid professionalId)
        {
            DateTime now = DateTime.UtcNow;

            IEnumerable<ExternalAppointment> pendingAppointments = await _repository.GetPendingExternalAppointmentsByUserIdAsync(professionalId);

            IEnumerable<ExternalAppointment> toCancel = pendingAppointments
                .Where(a => a.Date.ToUniversalTime() < now)
                .ToList();

            if (toCancel.Any())
            {
                foreach (ExternalAppointment appointment in toCancel)
                {
                    appointment.SetStatusCancelled();
                }

                await _repository.SaveChangesAsync();
            }
        }
    }
}
