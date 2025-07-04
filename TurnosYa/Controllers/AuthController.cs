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
        private readonly ICancelExpiredPendingAppointments _cancelExpiredPendingAppointments;
        private readonly ILoginHandler _loginHandler;
        public AuthController(ILoginHandler loginHandler, ICancelExpiredPendingAppointments cancelExpiredPendingAppointments) {
            _cancelExpiredPendingAppointments = cancelExpiredPendingAppointments;
            _loginHandler = loginHandler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequestDTO request)
        {
            try
            {
                LoginResponseDTO result = await _loginHandler.ExecuteAsync(request.Email, request.Password);
                await _cancelExpiredPendingAppointments.ExecuteAsync(result.UserId);
                return Ok(result);

            }
            catch(UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }
           
        }

    }
}
