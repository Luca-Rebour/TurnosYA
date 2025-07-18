using Application.DTOs.Appointment;
using Application.DTOs.Client;
using Application.DTOs.Professional;
using Application.Exceptions;
using Application.Interfaces.UseCases.Appointments;
using Application.Interfaces.UseCases.Professionals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/professionals")]
    [ApiController]
    public class ProfessionalController : Controller
    {
        private readonly ICreateProfessional _createProfessional;
        private readonly IGetProfessionalById _getProfessionalById;
        private readonly IUpdateProfessional _updateProfessional;
        private readonly IGetInternalAppointmentsByProfessionalId _getAppointmentsByProfessionalId;
        private readonly IGetProfessionalClients _getProfessionalClients;
        private readonly IGetProfessionalSummary _getProfessionalSummary;


        public ProfessionalController(
            ICreateProfessional createProfessional,
            IGetProfessionalById getProfessionalById,
            IUpdateProfessional updateProfessional,
            IGetInternalAppointmentsByProfessionalId getAppointmentsByProfessionalId,
            IGetProfessionalClients getProfessionalClients,
            IGetProfessionalSummary getProfessionalSummary
            )
        {
            _createProfessional = createProfessional;
            _getProfessionalById = getProfessionalById;
            _updateProfessional = updateProfessional;
            _getAppointmentsByProfessionalId = getAppointmentsByProfessionalId;
            _getProfessionalClients = getProfessionalClients;
            _getProfessionalSummary = getProfessionalSummary;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProfessionalDTO createProfessionalDTO)
        {
            try
            {
                ProfessionalDTO professional = await _createProfessional.ExecuteAsync(createProfessionalDTO);
                return Ok(professional);
            }
            catch (EmailAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            ProfessionalDTO result = await _getProfessionalById.ExecuteAsync(id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, UpdateProfessionalDTO updatedProfessional)
        {
            ProfessionalDTO result = await _updateProfessional.ExecuteAsync(id, updatedProfessional);
            return Ok(result);
        }

        [HttpGet("mine")]
        [Authorize(Roles = "professional")]
        public async Task<IActionResult> GetMyAppointments()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            IEnumerable<InternalAppointmentDTO> appointments = await _getAppointmentsByProfessionalId.ExecuteAsync(Guid.Parse(userId));

            return Ok(appointments);
        }


        [HttpGet("my-clients")]
        [Authorize]
        public async Task<IActionResult> GetMyClients()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            IEnumerable<ClientShortDTO> clients = await _getProfessionalClients.ExecuteAsync(Guid.Parse(userId));

            return Ok(clients);
        }

        [HttpGet("summary")]
        [Authorize]
        public async Task<IActionResult> GetSummary()
        {
            Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            SummaryDataDTO summary = await _getProfessionalSummary.ExecuteAsync(userId);

            return Ok(summary);
        }


    }
}
