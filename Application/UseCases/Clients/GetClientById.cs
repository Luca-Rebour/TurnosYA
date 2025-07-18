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
    public class GetClientById: IGetClientById
    {
        private IClientRepository _repository;

        private IMapper _mapper;
        public GetClientById(IClientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ClientDTO> ExecuteAsync(Guid id)
        {
            Client client = await _repository.GetByIdAsync(id);
            ClientDTO clientDTO = _mapper.Map<ClientDTO>(client);
            return clientDTO;
        }
    }
}
