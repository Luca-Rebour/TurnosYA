using Application.DTOs.Client;
using Application.DTOs.Professional;
using Application.Exceptions;
using Application.Interfaces.Services;
using Application.Interfaces.UseCases.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly ICreateClient _createClient;
        private readonly IGetClientById _getClientById;
        private readonly IUpdateClient _updateClient;


        public ClientController(
            ICreateClient createClient,
            IGetClientById getClientById,
            IUpdateClient updateClient
            )
        {
            _createClient = createClient;
            _getClientById = getClientById;
            _updateClient = updateClient;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientDTO CreateClientDTO)
        {
            try
            {
                ClientDTO client = await _createClient.ExecuteAsync(CreateClientDTO);
                return Ok(client);
            }
            catch (EmailAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            ClientDTO result = await _getClientById.ExecuteAsync(id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, UpdateClientDTO updatedClient)
        {
            ClientDTO result = await _updateClient.ExecuteAsync(id, updatedClient);
            return Ok(result);
        }
    }
}
