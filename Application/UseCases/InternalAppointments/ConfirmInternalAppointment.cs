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
    public class ConfirmInternalAppointment: IConfirmInternalAppointment
    {
        private IInternalAppointmentRepository _repository;
        public ConfirmInternalAppointment(IInternalAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Guid appointmentId)
        {
            InternalAppointment appointment = await _repository.GetByIdAsync(appointmentId);
            appointment.SetStatusConfirmed();
            await _repository.SaveChangesAsync();
        }

    }
}
