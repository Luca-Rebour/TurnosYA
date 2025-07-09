using Aplication.Interfaces.Repository;
using Application.DTOs.Auth;
using Application.Interfaces.Repository;
using Application.Interfaces.Security;
using Application.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Security
{
    public class LoginHandler: ILoginHandler
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProfessionalRepository _professionalRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginHandler(ICustomerRepository customerRepository, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator, IProfessionalRepository professionalRepository)
        {
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _customerRepository = customerRepository;
            _professionalRepository = professionalRepository;
        }

        public async Task<LoginResponseDTO> ExecuteAsync(string email, string password)
        {
            Customer? customer = await _customerRepository.GetByMailAsync(email);
            if (customer != null)
            {
                if (!_passwordHasher.Verify(password, customer.PasswordHash))
                    throw new UnauthorizedAccessException("Invalid credentials.");

                return new LoginResponseDTO
                {
                    Role = "Customer",
                    UserId = customer.Id,
                    Token = _tokenGenerator.GenerateToken(customer.Id.ToString(), customer.Name, "Customer")
                };
            }

            Professional? professional = await _professionalRepository.GetByMailAsync(email);
            if (professional != null)
            {
                if (!_passwordHasher.Verify(password, professional.PasswordHash))
                    throw new UnauthorizedAccessException("Invalid credentials.");

                return new LoginResponseDTO
                {
                    Role = "Professional",
                    UserId = professional.Id,
                    Token = _tokenGenerator.GenerateToken(professional.Id.ToString(), professional.Name, "Professional")
                };
            }

            throw new UnauthorizedAccessException("Invalid credentials.");
        }

    }
}
