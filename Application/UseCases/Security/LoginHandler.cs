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
        private readonly IClientRepository _clientRepository;
        private readonly IProfessionalRepository _professionalRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public LoginHandler(IClientRepository clientRepository, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator, IProfessionalRepository professionalRepository)
        {
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _clientRepository = clientRepository;
            _professionalRepository = professionalRepository;
        }

        public async Task<LoginResponseDTO> ExecuteAsync(string email, string password)
        {
            Client? client = await _clientRepository.GetByMailAsync(email);
            if (client != null)
            {
                if (!_passwordHasher.Verify(password, client.PasswordHash))
                    throw new UnauthorizedAccessException("Invalid credentials.");

                return new LoginResponseDTO
                {
                    Role = "Client",
                    UserId = client.Id,
                    Token = _tokenGenerator.GenerateToken(client.Id.ToString(), client.Name, "Client")
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
