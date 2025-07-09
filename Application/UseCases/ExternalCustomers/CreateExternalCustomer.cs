using Application.DTOs.Customer;
using Application.DTOs.ExternalCustomer;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.ExternalCustomers;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.ExternalCustomers
{
    public class CreateExternalCustomer : ICreateExternalCustomer
    {
        private readonly IExternalCustomerRepository _repository;
        private readonly IMapper _mapper;

        public CreateExternalCustomer(IExternalCustomerRepository repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ExternalCustomer> Execute(CreateExternalCustomerDTO dto)
        {
            ExternalCustomer newExternalCustomer = _mapper.Map<ExternalCustomer>(dto);

            await _repository.AddAsync(newExternalCustomer);
            return newExternalCustomer;
        }
    }
}
