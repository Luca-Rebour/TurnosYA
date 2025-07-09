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
    public class GetAppointmentById: IGetAppointmentById
    {
        private IAppointmentRepository _repository;
        private IMapper _mapper;
        public GetAppointmentById(IAppointmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AppointmentDTO> ExecuteAsync(Guid id)
        {
            Appointment appointment = await _repository.GetByIdAsync(id);
            return _mapper.Map<AppointmentDTO>(appointment);
        }


    }
}
