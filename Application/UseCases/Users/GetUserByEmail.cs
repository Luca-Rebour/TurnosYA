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
        private readonly ICustomerRepository _customerRepo;
        private readonly IProfessionalRepository _professionalRepo;

        public GetUserByEmail(ICustomerRepository customerRepo, IProfessionalRepository professionalRepo)
        {
            _customerRepo = customerRepo;
            _professionalRepo = professionalRepo;
        }

        public async Task<UserInternalDTO> ExecuteAsync(string email)
        {
            Customer customer = await _customerRepo.GetByMailAsync(email);
            if (customer != null)
            {
                return new UserInternalDTO
                {
                    Email = customer.Email,
                    Password = customer.PasswordHash,
                    Rol = "Customer"
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
