using Application.DTOs.Auth;
using Application.Interfaces.Security;
using Application.Interfaces.UseCases.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ICancelExpiredPendingInternalAppointments _cancelExpiredPendingAppointments;
        private readonly ILoginHandler _loginHandler;
        public AuthController(ILoginHandler loginHandler, ICancelExpiredPendingInternalAppointments cancelExpiredPendingAppointments) {
            _cancelExpiredPendingAppointments = cancelExpiredPendingAppointments;
            _loginHandler = loginHandler;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(
            [FromBody] LoginRequestDTO request)
        {
                LoginResponseDTO result = await _loginHandler.ExecuteAsync(request.Email, request.Password);
                await _cancelExpiredPendingAppointments.ExecuteAsync(result.UserId);
                return Ok(result);
           
        }

    }
}
