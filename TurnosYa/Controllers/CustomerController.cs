using Application.DTOs.Customer;
using Application.DTOs.Professional;
using Application.Exceptions;
using Application.Interfaces.Services;
using Application.Interfaces.UseCases.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICreateCustomer _createCustomer;
        private readonly IGetCustomerById _getCustomerById;
        private readonly IUpdateCustomer _updateCustomer;


        public CustomerController(
            ICreateCustomer createCustomer,
            IGetCustomerById getCustomerById,
            IUpdateCustomer updateCustomer
            )
        {
            _createCustomer = createCustomer;
            _getCustomerById = getCustomerById;
            _updateCustomer = updateCustomer;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDTO CreateCustomerDTO)
        {
            try
            {
                CustomerDTO customer = await _createCustomer.ExecuteAsync(CreateCustomerDTO);
                return Ok(customer);
            }
            catch (EmailAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            CustomerDTO result = await _getCustomerById.ExecuteAsync(id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, UpdateCustomerDTO updatedCustomer)
        {
            CustomerDTO result = await _updateCustomer.ExecuteAsync(id, updatedCustomer);
            return Ok(result);
        }
    }
}
