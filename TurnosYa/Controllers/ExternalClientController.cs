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
            try
            {
                Guid professionalId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                createExternalClientDTO.CreatedByProfessionalId = professionalId;

                ExternalClient ret = await _createExternalClient.Execute(createExternalClientDTO);
                return Ok(ret);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetExternalClients([FromQuery] ExternalClientFilterDto filter)
        {


            Guid professionalId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (!string.IsNullOrEmpty(filter.Email))
            {
                return Ok(await _getExternalClientByEmail.Execute(filter.Email, professionalId));
            }

            if (!string.IsNullOrEmpty(filter.Phone))
            {
                return Ok(await _getExternalClientByPhone.Execute(filter.Phone, professionalId));
            }

            if (filter.ProfessionalId.HasValue)
            {
                return Ok(await _getExternalClientsByProfessionalId.Execute(filter.ProfessionalId.Value));
            }

            return BadRequest("Debe especificar al menos un filtro.");

        }
    }
}
