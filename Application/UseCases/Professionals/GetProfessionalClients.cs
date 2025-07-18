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
        private IAppointmentRepository _appointmentRepository;
        private IMapper _mapper;
        public GetProfessionalClients(IProfessionalRepository repository, IMapper mapper, IAppointmentRepository appointmentRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }
        public async Task<IEnumerable<ClientShortDTO>> ExecuteAsync(Guid professionalId)
        {
            IEnumerable<Client> clients = await _appointmentRepository.GetClientsByProfessionalId(professionalId);
            IEnumerable<ClientShortDTO> clientsDTO = _mapper.Map<IEnumerable<ClientShortDTO>>(clients);
            return clientsDTO;
        }
    }
}
