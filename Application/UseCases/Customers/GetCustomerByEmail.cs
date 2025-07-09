using Application.DTOs.Customer;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.Customers;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.Customers
{
    public class GetCustomerByEmail: IGetCustomerByEmail
    {
        private ICustomerRepository _repository;

        private IMapper _mapper;
        public GetCustomerByEmail(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CustomerDTO> ExecuteAsync(string email)
        {
            Customer customer = await _repository.GetByMailAsync(email);
            CustomerDTO customerInternalDTO = _mapper.Map<CustomerDTO>(customer);
            return customerInternalDTO;
        }
    }
}
