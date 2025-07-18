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
        private IClientRepository _clientRepository;
        private IProfessionalRepository _professionalRepository;
        private ICreateUserActivity _createUserActivity;
        private IMapper _mapper;
        public CreateAppointment(IAppointmentRepository repository, IMapper mapper, IClientRepository clientRepository, IProfessionalRepository professionalRepository, ICreateUserActivity createUserActivity)
        {
            _repository = repository;
            _mapper = mapper;
            _clientRepository = clientRepository;
            _professionalRepository = professionalRepository;
            _createUserActivity = createUserActivity;
        }

        public async Task<AppointmentDTO> ExecuteAsync(CreateAppointmentDTO appointmentDTO)
        {
            Appointment appointment = _mapper.Map<Appointment>(appointmentDTO);

            Client client = await _clientRepository.GetByIdAsync(appointmentDTO.ClientId);
            Professional professional = await _professionalRepository.GetByIdAsync(appointmentDTO.ProfessionalId);

            appointment.SetClient(client);
            appointment.SetProfessional(professional);

            await _repository.AddAsync(appointment);

            await _createUserActivity.ExecuteAsync(appointment.Id, client.Id, professional.Id, ActivityType.AppointmentCreated, $"Client {client.Name} {client.LastName} booked an appointment for {appointment.Date:dd/MM/yyyy HH:mm}.");

            return _mapper.Map<AppointmentDTO>(appointment);
        }


    }
}
