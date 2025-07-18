using Application.DTOs.Appointment;
using Application.DTOs.Availability;
using Application.DTOs.Availibility;
using Application.DTOs.Professional;
using Application.Interfaces.Services;
using Application.Interfaces.UseCases.AvailabilitySlots;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/availabilities")]
    public class AvailabilitySlotController : Controller
    {
      
        private readonly ICreateAvailabilitySlot _createAvailabilitySlot;
        private readonly IUpdateAvailabilitySlot _updateAvailabilitySlot;

        public AvailabilitySlotController(
            ICreateAvailabilitySlot createAvailabilitySlot, 
            IUpdateAvailabilitySlot updateAvailabilitySlot)
        {
            _createAvailabilitySlot = createAvailabilitySlot;
            _updateAvailabilitySlot = updateAvailabilitySlot;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAvailabilitySlotDTO dto)
        {
            ProfessionalDTO professionalDTO = await _createAvailabilitySlot.ExecuteAsync(dto);
            return Ok(professionalDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, UpdateAvailabilitySlotDTO updateAvailabilitySlot)
        {
            ProfessionalDTO professionalDTO = await _updateAvailabilitySlot.ExecuteAsync(id, updateAvailabilitySlot);
            return Ok(professionalDTO);
        }
    }
}
