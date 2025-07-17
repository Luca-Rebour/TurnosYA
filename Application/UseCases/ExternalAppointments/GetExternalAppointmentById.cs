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

namespace Application.UseCases.ExternalAppointments
{
    public class GetExternalAppointmentById : IGetExternalAppointmentById
    {
        private IExternalAppointmentRepository _repository;
        private IMapper _mapper;
        public GetExternalAppointmentById(IExternalAppointmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ExternalAppointmentDTO> ExecuteAsync(Guid id)
        {
            ExternalAppointment appointment = await _repository.GetByIdAsync(id);
            return _mapper.Map<ExternalAppointmentDTO>(appointment);
        }


    }
}
