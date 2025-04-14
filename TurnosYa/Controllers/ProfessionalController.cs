using Application.DTOs.Appointment;
using Application.DTOs.Professional;
using Application.Interfaces.Services;
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
        public async Task<IActionResult> Create([FromBody] CreateProfessionalDTO dto)
        {
            ProfessionalDTO result = await _service.Create(dto);
            return Ok(result);
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
