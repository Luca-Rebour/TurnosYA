using Aplication.Interfaces.Repository;
using Application.DTOs.Availibility;
using Application.DTOs.Professional;
using Application.Interfaces.Security;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Application.Exceptions;
using Application.DTOs.Appointment;
using Application.DTOs.Customer;
using Application.Interfaces.Repository;


namespace Application.UseCases
{
    public class ProfessionalService : IProfessionalService
    {
        private readonly IProfessionalRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserEmailValidatorService _userEmailValidatorService;
        private readonly IAppointmentRepository _appointmentRepository;

        public ProfessionalService(IProfessionalRepository repository, IMapper mapper, IPasswordHasher passwordHasher, IUserEmailValidatorService userEmailValidatorService, IAppointmentRepository appointmentRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _userEmailValidatorService = userEmailValidatorService;
            _appointmentRepository = appointmentRepository;
        }


        public async Task<ProfessionalDTO> Create(CreateProfessionalDTO createProfessionalDTO)
        {
            if (await _userEmailValidatorService.EmailExists(createProfessionalDTO.Email))
            {
                throw new EmailAlreadyExistsException(createProfessionalDTO.Email);
            }
            string password = _passwordHasher.Hash(createProfessionalDTO.Password);

            Professional professional = _mapper.Map<Professional>(createProfessionalDTO);
            professional.SetPassword(password);

            await _repository.AddAsync(professional);
            return _mapper.Map<ProfessionalDTO>(professional);
        }

        public async Task Delete(Guid id)
        {
            await _repository.DeleteAsync(id);
        }


        public async Task<ProfessionalDTO> GetById(Guid id)
        {
            Professional professional = await _repository.GetByIdAsync(id);

            if (professional == null)
            {
                throw new Exception("Professional not found");
            }

            return _mapper.Map<ProfessionalDTO>(professional);
        }


        public async Task<ProfessionalDTO> Update(Guid id, UpdateProfessionalDTO dto)
        {
            Professional professional = await _repository.GetByIdAsync(id);
            if (professional == null)
            {
                throw new Exception("Profesional no encontrado");
            }

            _mapper.Map(dto, professional);

            await _repository.UpdateAsync(id, professional);

            return _mapper.Map<ProfessionalDTO>(professional);
        }

        public async Task<ProfessionalDTO> GetByEmail(string email)
        {
            Professional professional = await _repository.GetByMailAsync(email);

            if (professional == null)
            {
                throw new Exception("Profesional no encontrado");
            }

            return _mapper.Map<ProfessionalDTO>(professional);
        }
        public async Task<ProfessionalInternalDTO> GetByEmailInternal(string email)
        {
            Professional professional = await _repository.GetByMailAsync(email);


            return _mapper.Map<ProfessionalInternalDTO>(professional);
        }

        public async Task<IEnumerable<CustomerShortDTO>> GetCustomersAsync(Guid professionalId)
        {
            IEnumerable<Customer> customers = await _appointmentRepository.GetCustomersByProfessionalId(professionalId);
            IEnumerable<CustomerShortDTO> customersDTO = _mapper.Map<IEnumerable<CustomerShortDTO>>(customers);
            return customersDTO;
        }
    }

}
