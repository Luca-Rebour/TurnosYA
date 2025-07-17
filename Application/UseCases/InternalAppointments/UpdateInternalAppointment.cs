using Application.DTOs.Appointment;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.Appointments;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Appointments
{
    public class UpdateInternalAppointment: IUpdateInternalAppointment
    {
        private IInternalAppointmentRepository _repository;
        private IMapper _mapper;
        public UpdateInternalAppointment(IInternalAppointmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalAppointmentDTO> ExecuteAsync(Guid id, UpdateInternalAppointmentDTO updateAppointmentDTO)
        {
            InternalAppointment appointment = await _repository.GetByIdAsync(id);
            _mapper.Map(updateAppointmentDTO, appointment);
            await _repository.UpdateAsync(id, appointment);
            return _mapper.Map<InternalAppointmentDTO>(appointment);

        }
    }
}
