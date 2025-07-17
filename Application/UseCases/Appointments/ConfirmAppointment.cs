using Application.Exceptions;
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
    public class ConfirmAppointment : IConfirmAppointment
    {
        private readonly IGetAppointmentById _getAppointmentById;
        private readonly IAppointmentRepository _appointmentRepository;
        public ConfirmAppointment(IGetAppointmentById getAppointmentById, IAppointmentRepository appointmentRepository) { 
            _getAppointmentById = getAppointmentById;
            _appointmentRepository = appointmentRepository;
        }
        public async Task Execute(Guid id)
        {
            Appointment appointment = await _getAppointmentById.Execute(id);

            if (appointment == null)
            {
                throw new EntityNotFoundException($"There is no appointment with id {id}");
            }

            appointment.SetStatusConfirmed();
            _appointmentRepository.UpdateAsync(appointment);

            throw new NotImplementedException();
        }
    }
}
