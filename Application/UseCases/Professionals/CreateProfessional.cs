using Aplication.Interfaces.Repository;
using Application.DTOs.Professional;
using Application.Exceptions;
using Application.Interfaces.Security;
using Application.Interfaces.UseCases.Professionals;
using Application.Interfaces.Users;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.Professionals
{
    public class CreateProfessional: ICreateProfessional
    {
        private IProfessionalRepository _repository;
        private IValidateUserEmail _validateUserEmail;
        private IPasswordHasher _passwordHasher;
        private IMapper _mapper;
        public CreateProfessional(IProfessionalRepository repository, IValidateUserEmail validateUserEmail, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _validateUserEmail = validateUserEmail;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }
        public async Task<ProfessionalDTO> ExecuteAsync(CreateProfessionalDTO createProfessionalDTO)
        {
            if (await _validateUserEmail.ExecuteAsync(createProfessionalDTO.Email))
            {
                throw new EmailAlreadyExistsException(createProfessionalDTO.Email);
            }
            createProfessionalDTO.Validate();
            string password = _passwordHasher.Hash(createProfessionalDTO.Password);

            Professional professional = _mapper.Map<Professional>(createProfessionalDTO);
            professional.SetPassword(password);

            await _repository.AddAsync(professional);
            return _mapper.Map<ProfessionalDTO>(professional);
        }
    }
}
