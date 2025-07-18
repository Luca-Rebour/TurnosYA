using Application.DTOs.Client;
using Application.DTOs.ExternalClient;
using Application.Exceptions;
using Application.Interfaces.UseCases.Clients;
using Application.Interfaces.UseCases.ExternalClients;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/external-clients")]
    [ApiController]
    public class ExternalClientController : Controller
    {
        private readonly ICreateExternalClient _createExternalClient;
        private readonly IGetExternalClientByEmail _getExternalClientByEmail;
        private readonly IGetExternalClientByPhone _getExternalClientByPhone;
        private readonly IGetExternalClientsByProfessionalId _getExternalClientsByProfessionalId;


        public ExternalClientController(
            ICreateExternalClient createExternalClient,
            IGetExternalClientByEmail getExternalClientByEmail,
            IGetExternalClientByPhone getExternalClientByPhone,
            IGetExternalClientsByProfessionalId getExternalClientsByProfessionalId
            )
        {
            _createExternalClient = createExternalClient;
            _getExternalClientByEmail = getExternalClientByEmail;
            _getExternalClientByPhone = getExternalClientByPhone;
            _getExternalClientsByProfessionalId = getExternalClientsByProfessionalId;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateExternalClientDTO createExternalClientDTO)
        {
            Console.WriteLine("📦 Datos recibidos:");
            Console.WriteLine($"Name: {createExternalClientDTO.Name}");
            Console.WriteLine($"LastName: {createExternalClientDTO.LastName}");
            Console.WriteLine($"Phone: {createExternalClientDTO.Phone}");
            Console.WriteLine($"Email: {createExternalClientDTO.Email}");
            Console.WriteLine($"CreatedByProfessionalId: {createExternalClientDTO.CreatedByProfessionalId}");
            var claimValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimValue) || !Guid.TryParse(claimValue, out Guid professionalId))
            {
                Console.WriteLine("❌ No se pudo obtener un GUID válido desde el token.");
                return Unauthorized("Invalid or missing user ID in token.");
            }
            Console.WriteLine($"PROFESSIONAL ID: {claimValue}");
            createExternalClientDTO.CreatedByProfessionalId = professionalId;
            ExternalClient ret = await _createExternalClient.Execute(createExternalClientDTO);
            return Ok(ret);
        }

        [HttpGet("by-email")]
        [Authorize]
        public async Task<IActionResult> GetByEmail(string email)
        {
                Guid professionalId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                ExternalClient ret = await _getExternalClientByEmail.Execute(email, professionalId);
                return Ok(ret);
        
        }

        [HttpGet("by-phone")]
        [Authorize]
        public async Task<IActionResult> GetByPhone(string phone)
        {

                Guid professionalId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                ExternalClient ret = await _getExternalClientByPhone.Execute(phone, professionalId);
                return Ok(ret);
        }

        [HttpGet("by-professional")]
        [Authorize]
        public async Task<IActionResult> GetByProfessionalId()
        {
                Guid professionalId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                IEnumerable<ExternalClient> ret = await _getExternalClientsByProfessionalId.Execute(professionalId);
                return Ok(ret);

        }
    }
}
