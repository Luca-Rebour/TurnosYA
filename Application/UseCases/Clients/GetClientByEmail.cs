using Application.DTOs.Client;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.Clients;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.Clients
{
    public class GetClientByEmail: IGetClientByEmail
    {
        private IClientRepository _repository;

        private IMapper _mapper;
        public GetClientByEmail(IClientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ClientDTO> ExecuteAsync(string email)
        {
            Client client = await _repository.GetByMailAsync(email);
            ClientDTO clientInternalDTO = _mapper.Map<ClientDTO>(client);
            return clientInternalDTO;
        }
    }
}
