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
    public class GetAppointmentsByProfessionalId: IGetAppointmentsByProfessionalId
    {
        private IAppointmentRepository _repository;
        private IMapper _mapper;
        public GetAppointmentsByProfessionalId(IAppointmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentDTO>> ExecuteAsync(Guid id)
        {
            IEnumerable<Appointment> appointments = await _repository.GetAppointmentsByProfessionalId(id);
            IEnumerable<AppointmentDTO> result = _mapper.Map<IEnumerable<AppointmentDTO>>(appointments);
            return result;
        }

    }
}
