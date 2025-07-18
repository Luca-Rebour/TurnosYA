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
    [Route("api/external-appointments")]
    public class ExternalAppointmentController : ControllerBase
    {
        private readonly ICreateExternalAppointment _createExternalAppointment;
        private readonly IGetExternalAppointmentById _getExternalAppointmentById;
        private readonly IUpdateExternalAppointment _updateExternalAppointment;
        private readonly IGetExternalAppointmentsByProfessionalId _getExternalAppointmentsByProfessionalId;
        private readonly IConfirmExternalAppointment _confirmExternalAppointment;

        public ExternalAppointmentController(
            ICreateExternalAppointment createExternalAppointment,
            IGetExternalAppointmentById getExternalAppointmentById,
            IUpdateExternalAppointment updateExternalAppointment,
            IGetExternalAppointmentsByProfessionalId getExternalAppointmentsByProfessionalId,
            IConfirmExternalAppointment confirmExternalAppointment
            )
        {
            _createExternalAppointment = createExternalAppointment;
            _getExternalAppointmentById = getExternalAppointmentById;
            _updateExternalAppointment = updateExternalAppointment;
            _getExternalAppointmentById = getExternalAppointmentById;
            _getExternalAppointmentsByProfessionalId = getExternalAppointmentsByProfessionalId;
            _confirmExternalAppointment = confirmExternalAppointment;
            
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateExternalAppointmentDTO dto)
        {
            Guid professionalId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            dto.ProfessionalId = professionalId;
            ExternalAppointmentDTO result = await _createExternalAppointment.ExecuteAsync(dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            ExternalAppointmentDTO result = await _getExternalAppointmentById.ExecuteAsync(id);
            return Ok(result);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, UpdateExternalAppointmentDTO updatedExternalAppointment)
        {
            ExternalAppointmentDTO result = await _updateExternalAppointment.ExecuteAsync(id, updatedExternalAppointment);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserExternalAppointments()
        {   
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IEnumerable<ExternalAppointmentDTO> result = await _getExternalAppointmentsByProfessionalId.ExecuteAsync(Guid.Parse(userId));
            return Ok(result);
        }

        [HttpPatch("confirm/{id}")]
        [Authorize]
        public async Task<IActionResult> ConfirmExternalAppointment(Guid id)
        {
            ExternalAppointmentDTO appointment = await _getExternalAppointmentById.ExecuteAsync(id);
            await _confirmExternalAppointment.ExecuteAsync(id);
            return Ok();
        }

    }

}
