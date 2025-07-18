using Application.DTOs.Appointment;
using Application.DTOs.Availability;
using Application.DTOs.Availibility;
using Application.DTOs.Professional;
using Application.Interfaces.Services;
using Application.Interfaces.UseCases.AvailabilitySlots;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/availabilities")]
    public class AvailabilitySlotController : Controller
    {
      
        private readonly ICreateAvailabilitySlot _createAvailabilitySlot;
        private readonly IUpdateAvailabilitySlot _updateAvailabilitySlot;
        private readonly IGetAllAvailabilitySlots _getAllAvailabilitieSlots;
        private readonly IGetAllAvailabilitySlotsByProfessionalAndDay _getAllAvailabilitieSlotsByProfessionalAndDay;

        public AvailabilitySlotController(
            ICreateAvailabilitySlot createAvailabilitySlot, 
            IUpdateAvailabilitySlot updateAvailabilitySlot,
            IGetAllAvailabilitySlots getAllAvailabilitieSlots,
            IGetAllAvailabilitySlotsByProfessionalAndDay getAllAvailabilitieSlotsByProfessionalAndDay)
        {
            _createAvailabilitySlot = createAvailabilitySlot;
            _updateAvailabilitySlot = updateAvailabilitySlot;
            _getAllAvailabilitieSlots = getAllAvailabilitieSlots;
            _getAllAvailabilitieSlotsByProfessionalAndDay = getAllAvailabilitieSlotsByProfessionalAndDay;
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

        [HttpGet("{professionalId}/{date}")]
        [Authorize]
        public async Task<IActionResult> GetAll(Guid professionalId, DateTime date)
        {
            IEnumerable<AvailabilitySlot> availabilitySlotsDto = await _getAllAvailabilitieSlotsByProfessionalAndDay.Execute(professionalId, date);
            return Ok(availabilitySlotsDto);
        }
    }
}
