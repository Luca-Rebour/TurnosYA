using Application.DTOs.Customer;
using Application.DTOs.Professional;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDTO dto)
        {
            CustomerDTO result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            CustomerDTO result = await _service.GetById(id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, UpdateCustomerDTO updatedCustomer)
        {
            CustomerDTO result = await _service.Update(id, updatedCustomer);
            return Ok(result);
        }
    }
}
