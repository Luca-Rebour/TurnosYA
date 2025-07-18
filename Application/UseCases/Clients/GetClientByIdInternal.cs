using Application.DTOs.Client;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.Clients;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Clients
{
    public class GetClientByIdInternal: IGetClientByIdInternal
    {
        private IClientRepository _repository;

        private IMapper _mapper;
        public GetClientByIdInternal(IClientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ClientInternalDTO> ExecuteAsync(string email)
        {
            Client client = await _repository.GetByMailAsync(email);
            ClientInternalDTO clientInternalDTO = _mapper.Map<ClientInternalDTO>(client);
            return clientInternalDTO;
        }
    }
}
