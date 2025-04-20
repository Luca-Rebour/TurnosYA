using Application.DTOs.Appointment;
using Application.DTOs.UserActivity;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/activities")]
    [ApiController]
    public class UserActivitiesController : Controller
    {
        private readonly IUserActivityService _service;

        public UserActivitiesController(IUserActivityService service)
        {
            _service = service;
        }

        [HttpGet("userActivities")]
        public async Task<IActionResult> GetUserActivities()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IEnumerable<UserActivityDTO> result = await _service.GetAllAsync(Guid.Parse(userId));
            return Ok(result);
        }
    }
}
