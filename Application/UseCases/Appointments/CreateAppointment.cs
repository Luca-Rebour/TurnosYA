using Aplication.Interfaces.Repository;
using Application.DTOs.Appointment;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.Appointments;
using Application.Interfaces.Users;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCases.Appointments
{
    public class CreateAppointment: ICreateAppointment
    {
        private IAppointmentRepository _repository;
        private ICustomerRepository _customerRepository;
        private IProfessionalRepository _professionalRepository;
        private ICreateUserActivity _createUserActivity;
        private IMapper _mapper;
        public CreateAppointment(IAppointmentRepository repository, IMapper mapper, ICustomerRepository customerRepository, IProfessionalRepository professionalRepository, ICreateUserActivity createUserActivity)
        {
            _repository = repository;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _professionalRepository = professionalRepository;
            _createUserActivity = createUserActivity;
        }

        public async Task<AppointmentDTO> ExecuteAsync(CreateAppointmentDTO appointmentDTO)
        {
            Appointment appointment = _mapper.Map<Appointment>(appointmentDTO);

            Customer customer = await _customerRepository.GetByIdAsync(appointmentDTO.CustomerId);
            Professional professional = await _professionalRepository.GetByIdAsync(appointmentDTO.ProfessionalId);

            appointment.SetCustomer(customer);
            appointment.SetProfessional(professional);

            await _repository.AddAsync(appointment);

            await _createUserActivity.ExecuteAsync(appointment.Id, customer.Id, professional.Id, ActivityType.AppointmentCreated, $"Customer {customer.Name} {customer.LastName} booked an appointment for {appointment.Date:dd/MM/yyyy HH:mm}.");

            return _mapper.Map<AppointmentDTO>(appointment);
        }


    }
}
