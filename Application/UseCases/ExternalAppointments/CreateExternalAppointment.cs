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
        private IExternalClientRepository _externalClientRepository;
        private IProfessionalRepository _professionalRepository;
        private ICreateUserActivity _createUserActivity;
        private IAvailabilitySlotRepository _availabilitySlotRepository;
        private IMapper _mapper;
        public CreateExternalAppointment(IExternalAppointmentRepository repository, IMapper mapper, IExternalClientRepository externalClientRepository, IProfessionalRepository professionalRepository, ICreateUserActivity createUserActivity, IAvailabilitySlotRepository availabilitySlotRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _externalClientRepository = externalClientRepository;
            _professionalRepository = professionalRepository;
            _createUserActivity = createUserActivity;
            _availabilitySlotRepository = availabilitySlotRepository;
        }

        public async Task<ExternalAppointmentDTO> ExecuteAsync(CreateExternalAppointmentDTO appointmentDTO)
        {
            appointmentDTO.Validate();

            // Obtener entidades
            ExternalClient externalClient = await _externalClientRepository.GetByIdAsync(appointmentDTO.ExternalClientId);
            Professional professional = await _professionalRepository.GetByIdAsync(appointmentDTO.ProfessionalId);

            // Verificar disponibilidad
            var appointmentDay = appointmentDTO.Date.DayOfWeek;
            var appointmentTime = appointmentDTO.Date.TimeOfDay;

            AvailabilitySlot availableSlot = await _availabilitySlotRepository
                .GetAvailableSlotAsync(professional.Id, appointmentDay, appointmentTime);

            if (availableSlot == null)
            {
                throw new InvalidOperationException("No hay disponibilidad para la fecha y hora seleccionadas.");
            }

            availableSlot.MarkAsUnavailable();
            await _availabilitySlotRepository.UpdateAsync(availableSlot.Id, availableSlot);

            // Crear el appointment
            ExternalAppointment appointment = _mapper.Map<ExternalAppointment>(appointmentDTO);
            appointment.SetExternalClient(externalClient);
            appointment.SetProfessional(professional);

            await _repository.AddAsync(appointment);

            return _mapper.Map<ExternalAppointmentDTO>(appointment);
        }


    }
}
