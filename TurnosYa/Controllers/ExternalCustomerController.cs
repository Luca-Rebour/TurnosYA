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
    [Route("api/external-customer")]
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
            try
            {
                Guid professionalId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                createExternalCustomerDTO.CreatedByProfessionalId = professionalId;

                ExternalCustomer ret = await _createExternalCustomer.Execute(createExternalCustomerDTO);
                return Ok(ret);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("by-email")]
        [Authorize]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                Guid professionalId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                ExternalCustomer ret = await _getExternalCustomerByEmail.Execute(email, professionalId);
                return Ok(ret);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("by-phone")]
        [Authorize]
        public async Task<IActionResult> GetByPhone(string phone)
        {
            try
            {
                Guid professionalId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                ExternalCustomer ret = await _getExternalCustomerByPhone.Execute(phone, professionalId);
                return Ok(ret);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("by-professional")]
        [Authorize]
        public async Task<IActionResult> GetByProfessionalId()
        {
            try
            {
                Guid professionalId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                IEnumerable<ExternalCustomer> ret = await _getExternalCustomersByProfessionalId.Execute(professionalId);
                return Ok(ret);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
