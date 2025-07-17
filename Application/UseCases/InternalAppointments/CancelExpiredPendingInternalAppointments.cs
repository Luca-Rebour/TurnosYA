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
    public class CancelExpiredPendingInternalAppointments: ICancelExpiredPendingInternalAppointments
    {
        private IInternalAppointmentRepository _repository;
        public CancelExpiredPendingInternalAppointments(IInternalAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Guid professionalId)
        {
            DateTime now = DateTime.UtcNow;

            IEnumerable<InternalAppointment> pendingAppointments = await _repository.GetPendingInternalAppointmentsByUserIdAsync(professionalId);

            IEnumerable<InternalAppointment> toCancel = pendingAppointments
                .Where(a => a.Date.ToUniversalTime() < now)
                .ToList();

            if (toCancel.Any())
            {
                foreach (InternalAppointment appointment in toCancel)
                {
                    appointment.SetStatusCancelled();
                }

                await _repository.SaveChangesAsync();
            }
        }
    }
}
