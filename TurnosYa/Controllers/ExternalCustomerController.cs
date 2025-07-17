using Application.DTOs.Customer;
using Application.DTOs.ExternalCustomer;
using Application.Exceptions;
using Application.Interfaces.UseCases.Customers;
using Application.Interfaces.UseCases.ExternalCustomers;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/external-customers")]
    [ApiController]
    public class ExternalCustomerController : Controller
    {
        private readonly ICreateExternalCustomer _createExternalCustomer;
        private readonly IGetExternalCustomerByEmail _getExternalCustomerByEmail;
        private readonly IGetExternalCustomerByPhone _getExternalCustomerByPhone;
        private readonly IGetExternalCustomersByProfessionalId _getExternalCustomersByProfessionalId;


        public ExternalCustomerController(
            ICreateExternalCustomer createExternalCustomer,
            IGetExternalCustomerByEmail getExternalCustomerByEmail,
            IGetExternalCustomerByPhone getExternalCustomerByPhone,
            IGetExternalCustomersByProfessionalId getExternalCustomersByProfessionalId
            )
        {
            _createExternalCustomer = createExternalCustomer;
            _getExternalCustomerByEmail = getExternalCustomerByEmail;
            _getExternalCustomerByPhone = getExternalCustomerByPhone;
            _getExternalCustomersByProfessionalId = getExternalCustomersByProfessionalId;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateExternalCustomerDTO createExternalCustomerDTO)
        {
            Console.WriteLine("📦 Datos recibidos:");
            Console.WriteLine($"Name: {createExternalCustomerDTO.Name}");
            Console.WriteLine($"LastName: {createExternalCustomerDTO.LastName}");
            Console.WriteLine($"Phone: {createExternalCustomerDTO.Phone}");
            Console.WriteLine($"Email: {createExternalCustomerDTO.Email}");
            Console.WriteLine($"CreatedByProfessionalId: {createExternalCustomerDTO.CreatedByProfessionalId}");
            var claimValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimValue) || !Guid.TryParse(claimValue, out Guid professionalId))
            {
                Console.WriteLine("❌ No se pudo obtener un GUID válido desde el token.");
                return Unauthorized("Invalid or missing user ID in token.");
            }
            Console.WriteLine($"PROFESSIONAL ID: {claimValue}");
            createExternalCustomerDTO.CreatedByProfessionalId = professionalId;
            ExternalCustomer ret = await _createExternalCustomer.Execute(createExternalCustomerDTO);
            return Ok(ret);
        }

        [HttpGet("by-email")]
        [Authorize]
        public async Task<IActionResult> GetByEmail(string email)
        {
                Guid professionalId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                ExternalCustomer ret = await _getExternalCustomerByEmail.Execute(email, professionalId);
                return Ok(ret);
        
        }

        [HttpGet("by-phone")]
        [Authorize]
        public async Task<IActionResult> GetByPhone(string phone)
        {

                Guid professionalId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                ExternalCustomer ret = await _getExternalCustomerByPhone.Execute(phone, professionalId);
                return Ok(ret);
        }

        [HttpGet("by-professional")]
        [Authorize]
        public async Task<IActionResult> GetByProfessionalId()
        {
                Guid professionalId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                IEnumerable<ExternalCustomer> ret = await _getExternalCustomersByProfessionalId.Execute(professionalId);
                return Ok(ret);

        }
    }
}
