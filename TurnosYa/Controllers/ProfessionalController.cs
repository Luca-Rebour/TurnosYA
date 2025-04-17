using Application.DTOs.Appointment;
using Application.DTOs.Availability;
using Application.DTOs.Availibility;
using Application.DTOs.Customer;
using Application.DTOs.Professional;
using Application.Exceptions;
using Application.Interfaces.Services;
using Application.UseCases;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/professional")]
    [ApiController]
    public class ProfessionalController : Controller
    {
        private readonly IProfessionalService _service;
        private readonly IAppointmentService _appointmentService;

        public ProfessionalController(IProfessionalService service, IAppointmentService appointmentService)
        {
            _service = service;
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProfessionalDTO createProfessionalDTO)
        {
            try
            {
                ProfessionalDTO professional = await _service.Create(createProfessionalDTO);
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
            ProfessionalDTO result = await _service.GetById(id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, UpdateProfessionalDTO updatedProfessional)
        {
            ProfessionalDTO result = await _service.Update(id, updatedProfessional);
            return Ok(result);
        }

        [HttpGet("mine")]
        [Authorize(Roles = "professional")]
        public async Task<IActionResult> GetMyAppointments()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            IEnumerable<AppointmentDTO> appointments = await _appointmentService.GetAppointmentsByProfessionalId(Guid.Parse(userId));

            return Ok(appointments);
        }


        [HttpGet("mycustomers")]
        [Authorize(Roles = "professional")]
        public async Task<IActionResult> GetMyCustomers()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            IEnumerable<CustomerShortDTO> customers = await _service.GetCustomersAsync(Guid.Parse(userId));

            return Ok(customers);
        }


    }
}
