using Aplication.Interfaces.Repository;
using Application.Interfaces.Repository;
using Application.Interfaces.Users;
using Domain.Entities;

namespace Application.UseCases.User
{
    public class ValidateUserEmail: IValidateUserEmail
    {
        private readonly IClientRepository _clientRepo;
        private readonly IProfessionalRepository _professionalRepo;

        public ValidateUserEmail(IClientRepository clientRepo, IProfessionalRepository professionalRepo)
        {
            _clientRepo = clientRepo;
            _professionalRepo = professionalRepo;
        }

        public async Task<bool> ExecuteAsync(string email)
        {
            Client client = await _clientRepo.GetByMailAsync(email);
            Professional professional = await _professionalRepo.GetByMailAsync(email);
            return client != null || professional != null;
        }
    }
}
