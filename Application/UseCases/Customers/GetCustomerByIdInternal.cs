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
    public class GetCustomerByIdInternal: IGetCustomerByIdInternal
    {
        private ICustomerRepository _repository;

        private IMapper _mapper;
        public GetCustomerByIdInternal(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CustomerInternalDTO> ExecuteAsync(string email)
        {
            Customer customer = await _repository.GetByMailAsync(email);
            CustomerInternalDTO customerInternalDTO = _mapper.Map<CustomerInternalDTO>(customer);
            return customerInternalDTO;
        }
    }
}
