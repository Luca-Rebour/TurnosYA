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
    public class GetInternalAppointmentsByProfessionalId: IGetInternalAppointmentsByProfessionalId
    {
        private IInternalAppointmentRepository _repository;
        private IMapper _mapper;
        public GetInternalAppointmentsByProfessionalId(IInternalAppointmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InternalAppointmentDTO>> ExecuteAsync(Guid id)
        {
            IEnumerable<InternalAppointment> appointments = await _repository.GetInternalAppointmentsByProfessionalId(id);
            IEnumerable<InternalAppointmentDTO> result = _mapper.Map<IEnumerable<InternalAppointmentDTO>>(appointments);
            return result;
        }

    }
}
