using Application.DTOs.Auth;
using Application.DTOs.Customer;
using Application.DTOs.Professional;
using Application.Interfaces.Services;
using Application.Interfaces.Services.Security;
using Domain.Abstract;
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
        private readonly IAppointmentService _appointmentService;
        public AuthController(ICustomerService customerService, IProfessionalService professionalService, IAppointmentService appointmentService) {
            _customerService = customerService;
            _professionalService = professionalService;
            _appointmentService = appointmentService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequestDTO request,
            [FromServices] IEnumerable<ILoginHandler> handlers)
        {
            foreach (var handler in handlers)
            {
                LoginResponseDTO result = await handler.TryLoginAsync(request.Email, request.Password);
                if (result is not null)
                {
                    await _appointmentService.CancelExpiredPendingAppointmentsAsync(result.UserId);
                    return Ok(result);
                }
            }

            return Unauthorized();
        }


        //[HttpGet("me")]
        //public async Task<IActionResult> GetCurrentUser()
        //{
        //    Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        //    string role = User.FindFirst(ClaimTypes.Role)?.Value;
        //    _appointmentService.CancelExpiredPendingAppointmentsAsync(userId);

        //    if (role == "customer")
        //    {
        //        CustomerDTO customer = await _customerService.GetById(userId);
        //        return Ok(customer);
        //    }

        //    if (role == "professional")
        //    {
        //        ProfessionalDTO professional = await _professionalService.GetById(userId);
        //        return Ok(professional);
        //    }

        //    return Unauthorized();
        //}
    }
}
