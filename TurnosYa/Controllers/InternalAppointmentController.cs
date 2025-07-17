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
    [Route("api/internal-appointments")]
    public class InternalAppointmentController : ControllerBase
    {
        private readonly ICreateInternalAppointment _createAppointment;
        private readonly IGetInternalAppointmentById _getAppointmentById;
        private readonly IUpdateInternalAppointment _updateAppointment;
        private readonly IGetInternalAppointmentsByProfessionalId _getAppointmentsByProfessionalId;
        private readonly IConfirmInternalAppointment _confirmAppointment;

        public InternalAppointmentController(
            ICreateInternalAppointment createAppointment, 
            IGetInternalAppointmentById getAppointmentById,
            IUpdateInternalAppointment updateAppointment,
            IGetInternalAppointmentsByProfessionalId getAppointmentsByProfessionalId,
            IConfirmInternalAppointment confirmAppointment
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
        public async Task<IActionResult> Create([FromBody] CreateInternalAppointmentDTO dto)
        {
            InternalAppointmentDTO result = await _createAppointment.ExecuteAsync(dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            InternalAppointmentDTO result = await _getAppointmentById.ExecuteAsync(id);
            return Ok(result);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, UpdateInternalAppointmentDTO updatedAppointment)
        {
            InternalAppointmentDTO result = await _updateAppointment.ExecuteAsync(id, updatedAppointment);
            return Ok(result);
        }

        [HttpGet("user-internal-appointments")]
        [Authorize]
        public async Task<IActionResult> GetUserAppointments()
        {   
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IEnumerable<InternalAppointmentDTO> result = await _getAppointmentsByProfessionalId.ExecuteAsync(Guid.Parse(userId));
            return Ok(result);
        }

        [HttpPatch("confirm/{id}")]
        [Authorize]
        public async Task<IActionResult> ConfirmAppointment(Guid id)
        {
            InternalAppointmentDTO appointment = await _getAppointmentById.ExecuteAsync(id);
            await _confirmAppointment.ExecuteAsync(id);
            return Ok();
        }

    }

}
