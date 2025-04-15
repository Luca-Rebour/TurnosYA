using Application.DTOs.Customer;
using Application.Interfaces.Repository;
using Application.Interfaces.Security;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _repository;
        private IMapper _mapper;
        private IPasswordHasher _passwordHasher;
        public CustomerService(ICustomerRepository reposoitory, IMapper mapper, IPasswordHasher passwordHasher) 
        { 
            _repository = reposoitory;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }
        public async Task<CustomerDTO> Create(CreateCustomerDTO createCustomerDTO)
        {
            string passwordHash = _passwordHasher.Hash(createCustomerDTO.Password);

            Customer newCustomer = _mapper.Map<Customer>(createCustomerDTO);
            newCustomer.SetPassword(passwordHash);

            await _repository.AddAsync(newCustomer);
            return _mapper.Map<CustomerDTO>(newCustomer);
        }


        public async Task Delete(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<CustomerDTO> GetById(Guid id)
        {
            Customer customer = await _repository.GetByIdAsync(id);
            CustomerDTO customerDTO = _mapper.Map<CustomerDTO>(customer);
            return customerDTO;
        }

        public async Task<CustomerDTO> Update(Guid id, UpdateCustomerDTO updateCustomerDTO)
        {
            Customer customer = await _repository.GetByIdAsync(id);
            _mapper.Map(updateCustomerDTO, customer);
            await _repository.UpdateAsync(id, customer);
            CustomerDTO customerDTO = _mapper.Map<CustomerDTO>(customer);
            return customerDTO;
        }
        public async Task<CustomerInternalDTO> GetByEmailInternal(string email)
        {
            Customer customer = await _repository.GetByMailAsync(email);
            CustomerInternalDTO customerInternalDTO = _mapper.Map<CustomerInternalDTO>(customer);
            return customerInternalDTO;
        }
    }
}
