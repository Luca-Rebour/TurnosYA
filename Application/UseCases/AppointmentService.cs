using Aplication.Interfaces.Repository;
using Application.DTOs.Appointment;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class AppointmentService : IAppointmentService
    {
        private IAppointmentRepository _repository;
        private ICustomerRepository _customerRepository;
        private IProfessionalRepository _professionalRepository;
        private IMapper _mapper;
        public AppointmentService(IAppointmentRepository repository, IMapper mapper, ICustomerRepository customerRepository, IProfessionalRepository professionalRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _professionalRepository = professionalRepository;
        }
        public async Task<AppointmentDTO> Create(CreateAppointmentDTO appointmentDTO)
        {
            Appointment appointment = _mapper.Map<Appointment>(appointmentDTO);
            Customer customer = await _customerRepository.GetByIdAsync(appointmentDTO.CustomerId);
            Professional professional = await _professionalRepository.GetByIdAsync(appointmentDTO.ProfessionalId);
            appointment.SetCustomer(customer);
            appointment.SetProfessional(professional);
            await _repository.AddAsync(appointment);
            return _mapper.Map<AppointmentDTO>(appointment);
        }

        public async Task<AppointmentDTO> GetById(Guid id)
        {
            Appointment appointment = await _repository.GetByIdAsync(id);
            return _mapper.Map<AppointmentDTO>(appointment) ;
        }

        public async Task<AppointmentDTO> Update(Guid id, UpdateAppointmentDTO updateAppointmentDTO)
        {
            Appointment appointment = await _repository.GetByIdAsync(id);
            _mapper.Map(updateAppointmentDTO, appointment);
            await _repository.UpdateAsync(id, appointment);
            return _mapper.Map<AppointmentDTO>(appointment);

        }

        public async Task<IEnumerable<AppointmentDTO>> GetAppointmentsByProfessionalId(Guid id)
        {
            IEnumerable<Appointment> appointments = await _repository.GetAppointmentsByProfessionalId(id);
            IEnumerable<AppointmentDTO> result = _mapper.Map<IEnumerable<AppointmentDTO>>(appointments);
            return result;
        }
    }
}
