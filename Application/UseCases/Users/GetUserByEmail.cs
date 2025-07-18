using Aplication.Interfaces.Repository;
using Application.DTOs.User;
using Application.Exceptions;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.Users;
using Domain.Entities;


namespace Application.UseCases.Users
{
    public class GetUserByEmail: IGetUserByEmail
    {
        private readonly IClientRepository _clientRepo;
        private readonly IProfessionalRepository _professionalRepo;

        public GetUserByEmail(IClientRepository clientRepo, IProfessionalRepository professionalRepo)
        {
            _clientRepo = clientRepo;
            _professionalRepo = professionalRepo;
        }

        public async Task<UserInternalDTO> ExecuteAsync(string email)
        {
            Client client = await _clientRepo.GetByMailAsync(email);
            if (client != null)
            {
                return new UserInternalDTO
                {
                    Email = client.Email,
                    Password = client.PasswordHash,
                    Rol = "Client"
                };
            }

            Professional professional = await _professionalRepo.GetByMailAsync(email);
            if (professional != null)
            {
                return new UserInternalDTO
                {
                    Email = professional.Email,
                    Password = professional.PasswordHash,
                    Rol = "Professonal"
                };
            }

            throw new UserNotFoundException(email);
        }
    }
}
