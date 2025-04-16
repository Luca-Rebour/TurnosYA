using Aplication.Interfaces.Repository;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class UserEmailValidator : IUserEmailValidatorService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IProfessionalRepository _professionalRepo;

        public UserEmailValidator(ICustomerRepository customerRepo, IProfessionalRepository professionalRepo)
        {
            _customerRepo = customerRepo;
            _professionalRepo = professionalRepo;
        }

        public async Task<bool> EmailExists(string email)
        {
            Customer customer = await _customerRepo.GetByMailAsync(email);
            Professional professional = await _professionalRepo.GetByMailAsync(email);
            return customer != null || professional != null;
        }
    }

}
