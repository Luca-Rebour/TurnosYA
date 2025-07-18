using Aplication.Interfaces.Repository;
using Application.DTOs.Client;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.Professionals;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Professionals
{
    public class GetProfessionalClients: IGetProfessionalClients
    {
        private IProfessionalRepository _repository;
        private IInternalAppointmentRepository _internalAppointmentRepository;
        private IExternalAppointmentRepository _externalAppointmentRepository;
        private IMapper _mapper;
        public GetProfessionalClients(IProfessionalRepository repository, IMapper mapper, IInternalAppointmentRepository appointmentRepository, IExternalAppointmentRepository externalAppointmentRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _internalAppointmentRepository = appointmentRepository;
            _externalAppointmentRepository = externalAppointmentRepository;
        }
        public async Task<IEnumerable<ClientShortDTO>> ExecuteAsync(Guid professionalId)
        {
            var clients = await _internalAppointmentRepository.GetClientsByProfessionalId(professionalId);
            var externalClients = await _externalAppointmentRepository.GetClientsByProfessionalId(professionalId);

            var clientDTOs = _mapper.Map<IEnumerable<ClientShortDTO>>(clients);
            var externalClientDTOs = _mapper.Map<IEnumerable<ClientShortDTO>>(externalClients);

            return clientDTOs.Concat(externalClientDTOs);
        }

    }
}
