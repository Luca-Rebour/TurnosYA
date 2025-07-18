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
    public class CreateInternalAppointment: ICreateInternalAppointment
    {
        private IInternalAppointmentRepository _repository;
        private IClientRepository _clientRepository;
        private IProfessionalRepository _professionalRepository;
        private ICreateUserActivity _createUserActivity;
        private IMapper _mapper;
        private IAvailabilitySlotRepository _availabilitySlotRepository;
        public CreateInternalAppointment(IInternalAppointmentRepository repository, IMapper mapper, IClientRepository clientRepository, IProfessionalRepository professionalRepository, ICreateUserActivity createUserActivity, IAvailabilitySlotRepository availabilitySlotRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _clientRepository = clientRepository;
            _professionalRepository = professionalRepository;
            _createUserActivity = createUserActivity;
            _availabilitySlotRepository = availabilitySlotRepository;
        }

        public async Task<InternalAppointmentDTO> ExecuteAsync(CreateInternalAppointmentDTO appointmentDTO)
        {
            appointmentDTO.Validate();

            // Obtener entidades
            Client client = await _clientRepository.GetByIdAsync(appointmentDTO.ClientId);
            Professional professional = await _professionalRepository.GetByIdAsync(appointmentDTO.ProfessionalId);

            // Verificar disponibilidad
            var appointmentDay = appointmentDTO.Date.DayOfWeek;
            var appointmentTime = appointmentDTO.Date.TimeOfDay;

            var availableSlot = await _availabilitySlotRepository
                .GetAvailableSlotAsync(professional.Id, appointmentDay, appointmentTime);

            if (availableSlot == null)
            {
                throw new InvalidOperationException("No hay disponibilidad para la fecha y hora seleccionadas.");
            }

            availableSlot.MarkAsUnavailable();
            await _availabilitySlotRepository.UpdateAsync(availableSlot.Id, availableSlot);

            // Crear el appointment
            InternalAppointment appointment = _mapper.Map<InternalAppointment>(appointmentDTO);
            appointment.SetInternalClient(client);
            appointment.SetProfessional(professional);

            await _repository.AddAsync(appointment);

            // Registrar actividad del usuario
            await _createUserActivity.ExecuteAsync(
                appointment.Id,
                client.Id,
                professional.Id,
                ActivityType.AppointmentCreated,
                $"Client {client.Name} {client.LastName} booked an appointment for {appointment.Date:dd/MM/yyyy HH:mm}."
            );

            return _mapper.Map<InternalAppointmentDTO>(appointment);
        }


    }
}
