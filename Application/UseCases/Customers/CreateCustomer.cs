using Aplication.Interfaces.Repository;
using Application.DTOs.Customer;
using Application.Exceptions;
using Application.Interfaces.Repository;
using Application.Interfaces.Security;
using Application.Interfaces.UseCases.Customers;
using Application.Interfaces.Users;
using AutoMapper;
using Domain.Entities;


namespace Application.UseCases.Customers
{
    public class CreateCustomer: ICreateCustomer
    {
        private ICustomerRepository _repository;
        private IValidateUserEmail _validateUserEmail;
        private IPasswordHasher _passwordHasher;
        private IMapper _mapper;
        public CreateCustomer(ICustomerRepository repository, IValidateUserEmail validateUserEmail, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _validateUserEmail = validateUserEmail;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }
        public async Task<CustomerDTO> ExecuteAsync(CreateCustomerDTO createCustomerDTO)
        {

            if (await _validateUserEmail.ExecuteAsync(createCustomerDTO.Email))
            {
                throw new EmailAlreadyExistsException(createCustomerDTO.Email);
            }

            createCustomerDTO.Validate();

            string passwordHash = _passwordHasher.Hash(createCustomerDTO.Password);

            Customer newCustomer = _mapper.Map<Customer>(createCustomerDTO);
            newCustomer.SetPassword(passwordHash);

            await _repository.AddAsync(newCustomer);
            return _mapper.Map<CustomerDTO>(newCustomer);
        }
    }
}
