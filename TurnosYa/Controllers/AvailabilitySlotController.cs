using Application.DTOs.Appointment;
using Application.DTOs.Availability;
using Application.DTOs.Availibility;
using Application.DTOs.Professional;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/availability")]
    public class AvailabilitySlotController : Controller
    {
      
        private readonly IAvailabilitySlotService _service;

        public AvailabilitySlotController(IAvailabilitySlotService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAvailabilitySlotDTO dto)
        {
            ProfessionalDTO professionalDTO = await _service.Create(dto);
            return Ok(professionalDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, UpdateAvailabilitySlotDTO updateAvailabilitySlot)
        {
            ProfessionalDTO professionalDTO = await _service.Update(id, updateAvailabilitySlot);
            return Ok(professionalDTO);
        }
    }
}
