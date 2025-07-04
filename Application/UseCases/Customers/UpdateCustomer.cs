using Application.DTOs.Customer;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.Customers;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Customers
{
    public class UpdateCustomer: IUpdateCustomer
    {
        private ICustomerRepository _repository;
        private IMapper _mapper;
        public UpdateCustomer(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CustomerDTO> ExecuteAsync(Guid id, UpdateCustomerDTO updateCustomerDTO)
        {
            Customer customer = await _repository.GetByIdAsync(id);
            _mapper.Map(updateCustomerDTO, customer);
            await _repository.UpdateAsync(id, customer);
            CustomerDTO customerDTO = _mapper.Map<CustomerDTO>(customer);
            return customerDTO;
        }
    }
}
