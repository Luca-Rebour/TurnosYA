using Aplication.Interfaces.Repository;
using Application.DTOs.Client;
using Application.Exceptions;
using Application.Interfaces.Repository;
using Application.Interfaces.Security;
using Application.Interfaces.UseCases.Clients;
using Application.Interfaces.Users;
using AutoMapper;
using Domain.Entities;


namespace Application.UseCases.Clients
{
    public class CreateClient: ICreateClient
    {
        private IClientRepository _repository;
        private IValidateUserEmail _validateUserEmail;
        private IPasswordHasher _passwordHasher;
        private IMapper _mapper;
        public CreateClient(IClientRepository repository, IValidateUserEmail validateUserEmail, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _validateUserEmail = validateUserEmail;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }
        public async Task<ClientDTO> ExecuteAsync(CreateClientDTO createClientDTO)
        {

            if (await _validateUserEmail.ExecuteAsync(createClientDTO.Email))
            {
                throw new EmailAlreadyExistsException(createClientDTO.Email);
            }

            createClientDTO.Validate();

            string passwordHash = _passwordHasher.Hash(createClientDTO.Password);

            Client newClient = _mapper.Map<Client>(createClientDTO);
            newClient.SetPassword(passwordHash);

            await _repository.AddAsync(newClient);
            return _mapper.Map<ClientDTO>(newClient);
        }
    }
}
