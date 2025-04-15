using Application.DTOs.Customer;
using Application.Interfaces.Repository;
using Application.Interfaces.Security;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Application.Exceptions;

namespace Application.UseCases
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserEmailValidatorService _userEmailValidatorService;
        public CustomerService(ICustomerRepository reposoitory, IMapper mapper, IPasswordHasher passwordHasher, IUserEmailValidatorService userEmailValidatorService) 
        { 
            _repository = reposoitory;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _userEmailValidatorService = userEmailValidatorService;
        }
        public async Task<CustomerDTO> Create(CreateCustomerDTO createCustomerDTO)
        {

            if (await _userEmailValidatorService.EmailExists(createCustomerDTO.Email))
            {
                throw new EmailAlreadyExistsException(createCustomerDTO.Email);
            }

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
        public async Task<CustomerDTO> GetByEmail(string email)
        {
            Customer customer = await _repository.GetByMailAsync(email);
            CustomerDTO customerInternalDTO = _mapper.Map<CustomerDTO>(customer);
            return customerInternalDTO;
        }
        public async Task<CustomerInternalDTO> GetByEmailInternal(string email)
        {
            Customer customer = await _repository.GetByMailAsync(email);
            CustomerInternalDTO customerInternalDTO = _mapper.Map<CustomerInternalDTO>(customer);
            return customerInternalDTO;
        }

    }
}
