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
    public class ConfirmExternalAppointment : IConfirmExternalAppointment
    {
        private IExternalAppointmentRepository _repository;
        public ConfirmExternalAppointment(IExternalAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Guid appointmentId)
        {
            ExternalAppointment appointment = await _repository.GetByIdAsync(appointmentId);
            appointment.SetStatusConfirmed();
            await _repository.SaveChangesAsync();
        }

    }
}
