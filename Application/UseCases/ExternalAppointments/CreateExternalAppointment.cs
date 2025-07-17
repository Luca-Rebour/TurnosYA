using Aplication.Interfaces.Repository;
using Application.DTOs.Appointment;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.Appointments;
using Application.Interfaces.Users;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCases.ExternalAppointments
{
    public class CreateExternalAppointment : ICreateExternalAppointment
    {
        private IExternalAppointmentRepository _repository;
        private IExternalCustomerRepository _ExternalcustomerRepository;
        private IProfessionalRepository _professionalRepository;
        private ICreateUserActivity _createUserActivity;
        private IMapper _mapper;
        public CreateExternalAppointment(IExternalAppointmentRepository repository, IMapper mapper, IExternalCustomerRepository externalCustomerRepository, IProfessionalRepository professionalRepository, ICreateUserActivity createUserActivity)
        {
            _repository = repository;
            _mapper = mapper;
            _ExternalcustomerRepository = externalCustomerRepository;
            _professionalRepository = professionalRepository;
            _createUserActivity = createUserActivity;
        }

        public async Task<ExternalAppointmentDTO> ExecuteAsync(CreateExternalAppointmentDTO appointmentDTO)
        {
            appointmentDTO.Validate();

            ExternalAppointment appointment = _mapper.Map<ExternalAppointment>(appointmentDTO);

            ExternalCustomer customer = await _ExternalcustomerRepository.GetByIdAsync(appointmentDTO.ExternalCustomerId);
            Professional professional = await _professionalRepository.GetByIdAsync(appointmentDTO.ProfessionalId);
            

            appointment.SetCustomer(customer);
            appointment.SetProfessional(professional);

            await _repository.AddAsync(appointment);

            return _mapper.Map<ExternalAppointmentDTO>(appointment);
        }


    }
}
