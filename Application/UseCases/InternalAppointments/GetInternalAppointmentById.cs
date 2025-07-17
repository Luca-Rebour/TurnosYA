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
    public class GetInternalAppointmentById: IGetInternalAppointmentById
    {
        private IInternalAppointmentRepository _repository;
        private IMapper _mapper;
        public GetInternalAppointmentById(IInternalAppointmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalAppointmentDTO> ExecuteAsync(Guid id)
        {
            InternalAppointment appointment = await _repository.GetByIdAsync(id);
            return _mapper.Map<InternalAppointmentDTO>(appointment);
        }


    }
}
