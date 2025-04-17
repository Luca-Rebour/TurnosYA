using Application.DTOs.Appointment;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDTO dto)
        {
            AppointmentDTO result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            AppointmentDTO result = await _service.GetById(id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, UpdateAppointmentDTO updatedAppointment)
        {
            AppointmentDTO result = await _service.Update(id, updatedAppointment);
            return Ok(result);
        }

        [HttpGet("userAppointments")]
        public async Task<IActionResult> GetUserAppointments()
        {   
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IEnumerable<AppointmentDTO> result = await _service.GetAppointmentsByProfessionalId(Guid.Parse(userId));
            return Ok(result);
        }

    }

}
