using Aplication.Interfaces.Repository;
using Application.DTOs.Appointment;
using Application.DTOs.Professional;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.Professionals;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Professionals
{
    public class GetProfessionalSummary : IGetProfessionalSummary
    {
        private IProfessionalRepository _professionalRepository;
        private IInternalAppointmentRepository _internalAppointmentRepository;
        private IExternalAppointmentRepository _externalAppointmentRepository;
        private IMapper _mapper;
        public GetProfessionalSummary(IProfessionalRepository repository, IMapper mapper, IInternalAppointmentRepository internalAppointmentRepository, IExternalAppointmentRepository externalAppointmentRepository)
        {
            _professionalRepository = repository;
            _mapper = mapper;
            _internalAppointmentRepository = internalAppointmentRepository;
            _externalAppointmentRepository = externalAppointmentRepository;
        }
        public async Task<SummaryDataDTO> ExecuteAsync(Guid professionalId)
        {
            IEnumerable<InternalAppointment> internalAppointents = await _internalAppointmentRepository.GetThisWeekInternalAppointmentsByIdAsync(professionalId);
            IEnumerable<ExternalAppointment> externalAppointments = await _externalAppointmentRepository.GetThisWeekExternalAppointmentsByIdAsync(professionalId);
            int activeClients = await _professionalRepository.GetActiveClients(professionalId);

            int todayAppointments = internalAppointents.Where(a => a.Date == DateTime.Now).Count();
            todayAppointments += externalAppointments.Where(a => a.Date == DateTime.Now).Count();

            int thisWeekAppointments = internalAppointents.Count();
            thisWeekAppointments += externalAppointments.Count();

            int pendingConfirmations = internalAppointents.Where(a => a.Status == 0).Count();
            pendingConfirmations +=externalAppointments.Where(a => a.Status == 0).Count();

            SummaryDataDTO ret = new SummaryDataDTO(todayAppointments, thisWeekAppointments, activeClients, pendingConfirmations);
            return ret;

        }
    }
}
