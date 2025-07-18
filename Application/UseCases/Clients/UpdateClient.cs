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
    public class UpdateClient: IUpdateClient
    {
        private IClientRepository _repository;
        private IMapper _mapper;
        public UpdateClient(IClientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ClientDTO> ExecuteAsync(Guid id, UpdateClientDTO updateClientDTO)
        {
            Client client = await _repository.GetByIdAsync(id);
            _mapper.Map(updateClientDTO, client);
            await _repository.UpdateAsync(id, client);
            ClientDTO clientDTO = _mapper.Map<ClientDTO>(client);
            return clientDTO;
        }
    }
}
