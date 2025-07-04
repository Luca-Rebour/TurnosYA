using Application.DTOs.Appointment;
using Application.DTOs.Customer;
using Application.DTOs.Professional;
using Application.Exceptions;
using Application.Interfaces.UseCases.Appointments;
using Application.Interfaces.UseCases.Professionals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/professional")]
    [ApiController]
    public class ProfessionalController : Controller
    {
        private readonly ICreateProfessional _createProfessional;
        private readonly IGetProfessionalById _getProfessionalById;
        private readonly IUpdateProfessional _updateProfessional;
        private readonly IGetAppointmentsByProfessionalId _getAppointmentsByProfessionalId;
        private readonly IGetProfessionalCustomers _getProfessionalCustomers;


        public ProfessionalController(
            ICreateProfessional createProfessional,
            IGetProfessionalById getProfessionalById,
            IUpdateProfessional updateProfessional,
            IGetAppointmentsByProfessionalId getAppointmentsByProfessionalId,
            IGetProfessionalCustomers getProfessionalCustomers
            )
        {
            _createProfessional = createProfessional;
            _getProfessionalById = getProfessionalById;
            _updateProfessional = updateProfessional;
            _getAppointmentsByProfessionalId = getAppointmentsByProfessionalId;
            _getProfessionalCustomers = getProfessionalCustomers;
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

            IEnumerable<AppointmentDTO> appointments = await _getAppointmentsByProfessionalId.ExecuteAsync(Guid.Parse(userId));

            return Ok(appointments);
        }


        [HttpGet("mycustomers")]
        [Authorize(Roles = "professional")]
        public async Task<IActionResult> GetMyCustomers()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            IEnumerable<CustomerShortDTO> customers = await _getProfessionalCustomers.ExecuteAsync(Guid.Parse(userId));

            return Ok(customers);
        }


    }
}
