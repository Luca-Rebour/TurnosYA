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
    public class GetExternalAppointmentsByProfessionalId : IGetExternalAppointmentsByProfessionalId
    {
        private IExternalAppointmentRepository _repository;
        private IMapper _mapper;
        public GetExternalAppointmentsByProfessionalId(IExternalAppointmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExternalAppointmentDTO>> ExecuteAsync(Guid id)
        {
            IEnumerable<ExternalAppointment> appointments = await _repository.GetExternalAppointmentsByProfessionalId(id);
            IEnumerable<ExternalAppointmentDTO> result = _mapper.Map<IEnumerable<ExternalAppointmentDTO>>(appointments);
            return result;
        }

    }
}
