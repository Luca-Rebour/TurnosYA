using Application.Interfaces.UseCases.Appointments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentController : Controller
    {
        private readonly IConfirmAppointment _confirmAppointment;
        public AppointmentController(IConfirmAppointment confirmAppointment) {
            _confirmAppointment = confirmAppointment;
        }
        [HttpPatch("confirm/{id}")]
        [Authorize]
        public IActionResult ConfirmAppointment(Guid id)
        {
            _confirmAppointment.Execute(id);
            return Ok();
        }
    }
}
