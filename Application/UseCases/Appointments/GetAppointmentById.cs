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
    public class GetAppointmentById: IGetAppointmentById
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public GetAppointmentById(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Appointment> Execute(Guid id)
        {
            Appointment ap = await _appointmentRepository.GetByIdAsync(id);
            if (ap != null)
            {
                throw new EntityNotFoundException($"There is no appointment with id {id}");
            }

            return ap;
        }
    }
}
