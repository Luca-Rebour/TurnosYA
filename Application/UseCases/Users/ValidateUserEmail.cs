using Aplication.Interfaces.Repository;
using Application.Interfaces.Repository;
using Application.Interfaces.Users;
using Domain.Entities;

namespace Application.UseCases.User
{
    public class ValidateUserEmail: IValidateUserEmail
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IProfessionalRepository _professionalRepo;

        public ValidateUserEmail(ICustomerRepository customerRepo, IProfessionalRepository professionalRepo)
        {
            _customerRepo = customerRepo;
            _professionalRepo = professionalRepo;
        }

        public async Task<bool> ExecuteAsync(string email)
        {
            Customer customer = await _customerRepo.GetByMailAsync(email);
            Professional professional = await _professionalRepo.GetByMailAsync(email);
            return customer != null || professional != null;
        }
    }
}
