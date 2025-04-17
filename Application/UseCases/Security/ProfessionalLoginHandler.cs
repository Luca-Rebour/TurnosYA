using Application.DTOs.Auth;
using Application.Interfaces.Services.Security;
using Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Security
{
    public class ProfessionalLoginHandler : ILoginHandler
    {
        private readonly IProfessionalService _professionalService;
        private readonly IAuthPasswordService _passwordService;
        private readonly IJwtService _jwtService;

        public ProfessionalLoginHandler(
            IProfessionalService professionalService,
            IAuthPasswordService passwordService,
            IJwtService jwtService)
        {
            _professionalService = professionalService;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDTO?> TryLoginAsync(string email, string password)
        {
            var professional = await _professionalService.GetByEmailInternal(email);

            if (professional != null && _passwordService.VerifyPassword(professional.PasswordHash, password))
            {
                var token = _jwtService.GenerateToken(professional.Id.ToString(), professional.Name, "professional");
                return new LoginResponseDTO
                {
                    Token = token,
                    Role = "professional",
                    UserId = professional.Id
                };
            }

            return null;
        }
    }


}
