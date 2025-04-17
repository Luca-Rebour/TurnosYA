using Application.DTOs.Auth;
using Application.DTOs.Customer;
using Application.DTOs.Professional;
using Application.Interfaces.Services;
using Application.Interfaces.Services.Security;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IProfessionalService _professionalService;
        public AuthController(ICustomerService customerService, IProfessionalService professionalService) {
            _customerService = customerService;
            _professionalService = professionalService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequestDTO request,
            [FromServices] IEnumerable<ILoginHandler> handlers)
        {
            foreach (var handler in handlers)
            {
                var result = await handler.TryLoginAsync(request.Email, request.Password);
                if (result is not null)
                {
                    return Ok(result);
                }
            }

            return Unauthorized();
        }


        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role == "customer")
            {
                CustomerDTO customer = await _customerService.GetById(Guid.Parse(userId));
                return Ok(customer);
            }

            if (role == "professional")
            {
                ProfessionalDTO professional = await _professionalService.GetById(Guid.Parse(userId));
                return Ok(professional);
            }

            return Unauthorized();
        }
    }
}
