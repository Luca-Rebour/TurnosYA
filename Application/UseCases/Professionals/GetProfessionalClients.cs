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
            IEnumerable<Client> internalClients = await _internalAppointmentRepository.GetClientsByProfessionalId(professionalId);
            IEnumerable<ExternalClient> externalClients = await _externalAppointmentRepository.GetClientsByProfessionalId(professionalId);

            List<ClientShortDTO> internalClientDTOs = _mapper.Map<List<ClientShortDTO>>(internalClients);
            List<ClientShortDTO> externalClientDTOs = _mapper.Map<List<ClientShortDTO>>(externalClients);

            internalClientDTOs.ForEach(c => c.ClientType = "Internal");
            externalClientDTOs.ForEach(c => c.ClientType = "External");

            return internalClientDTOs.Concat(externalClientDTOs);
        }


    }
}
