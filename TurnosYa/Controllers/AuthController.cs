using Application.DTOs.Auth;
using Application.Interfaces.Services.Security;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
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
    }
}
