using Application.DTOs.UserActivity;
using Application.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/activities")]
    [ApiController]
    public class UserActivitiesController : Controller
    {
        private readonly IGetAllUserActivities _getAllUserActivities;

        public UserActivitiesController(
            IGetAllUserActivities getAllUserActivities
            )
        {
            _getAllUserActivities = getAllUserActivities;
        }

        [HttpGet("userActivities")]
        public async Task<IActionResult> GetUserActivities()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IEnumerable<UserActivityDTO> result = await _getAllUserActivities.ExecuteAsync(Guid.Parse(userId));
            return Ok(result);
        }
    }
}
