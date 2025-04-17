using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Auth;
using Application.Interfaces.Services;
using Application.Interfaces.Services.Security;

namespace Application.UseCases.Security
{
    public class CustomerLoginHandler : ILoginHandler
    {
        private readonly ICustomerService _customerService;
        private readonly IAuthPasswordService _authPasswordService;
        private readonly IJwtService _jwtService;

        public CustomerLoginHandler(
            ICustomerService customerService,
            IAuthPasswordService authPasswordService,
            IJwtService jwtService)
        {
            _customerService = customerService;
            _authPasswordService = authPasswordService;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDTO?> TryLoginAsync(string email, string password)
        {
            var customer = await _customerService.GetByEmailInternal(email);
            if (customer is null || !_authPasswordService.VerifyPassword(customer.PasswordHash, password))
                return null;

            var token = _jwtService.GenerateToken(customer.Id.ToString(), customer.Name, "customer");

            return new LoginResponseDTO
            {
                Token = token,
                Role = "customer",
                UserId = customer.Id
            };
        }
    }
}
