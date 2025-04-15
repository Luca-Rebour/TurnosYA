using Application.DTOs.Appointment;
using Application.DTOs.Availability;
using Application.DTOs.Availibility;
using Application.DTOs.Professional;
using Application.Exceptions;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/professional")]
    [ApiController]
    public class ProfessionalController : Controller
    {
        private readonly IProfessionalService _service;

        public ProfessionalController(IProfessionalService service)
        {
            _service = service;
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

    }
}
