using Application.DTOs.Appointment;
using Application.Interfaces.Services;
using Application.Interfaces.UseCases.Appointments;
using Application.UseCases.Appointments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/appointment")]
    public class AppointmentController : ControllerBase
    {
        private readonly ICreateAppointment _createAppointment;
        private readonly IGetAppointmentById _getAppointmentById;
        private readonly IUpdateAppointment _updateAppointment;
        private readonly IGetAppointmentsByProfessionalId _getAppointmentsByProfessionalId;
        private readonly IConfirmAppointment _confirmAppointment;

        public AppointmentController(
            ICreateAppointment createAppointment, 
            IGetAppointmentById getAppointmentById,
            IUpdateAppointment updateAppointment,
            IGetAppointmentsByProfessionalId getAppointmentsByProfessionalId,
            IConfirmAppointment confirmAppointment
            )
        {
            _createAppointment = createAppointment;
            _getAppointmentById = getAppointmentById;
            _updateAppointment = updateAppointment;
            _getAppointmentById = getAppointmentById;
            _getAppointmentsByProfessionalId = getAppointmentsByProfessionalId;
            _confirmAppointment = confirmAppointment;
            
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDTO dto)
        {
            AppointmentDTO result = await _createAppointment.ExecuteAsync(dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            AppointmentDTO result = await _getAppointmentById.ExecuteAsync(id);
            return Ok(result);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, UpdateAppointmentDTO updatedAppointment)
        {
            AppointmentDTO result = await _updateAppointment.ExecuteAsync(id, updatedAppointment);
            return Ok(result);
        }

        [HttpGet("userAppointments")]
        [Authorize]
        public async Task<IActionResult> GetUserAppointments()
        {   
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IEnumerable<AppointmentDTO> result = await _getAppointmentsByProfessionalId.ExecuteAsync(Guid.Parse(userId));
            return Ok(result);
        }

        [HttpPatch("appointments/{id}")]
        [Authorize]
        public async Task<IActionResult> ConfirmAppointment(Guid id)
        {
            AppointmentDTO appointment = await _getAppointmentById.ExecuteAsync(id);
            await _confirmAppointment.ExecuteAsync(id);
            return Ok();
        }

    }

}
