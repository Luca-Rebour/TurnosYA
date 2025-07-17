using Aplication.Interfaces.Repository;
using Application.DTOs.Customer;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.Professionals;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Professionals
{
    public class GetProfessionalCustomers: IGetProfessionalCustomers
    {
        private IProfessionalRepository _repository;
        private IInternalAppointmentRepository _internalAppointmentRepository;
        private IExternalAppointmentRepository _externalAppointmentRepository;
        private IMapper _mapper;
        public GetProfessionalCustomers(IProfessionalRepository repository, IMapper mapper, IInternalAppointmentRepository appointmentRepository, IExternalAppointmentRepository externalAppointmentRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _internalAppointmentRepository = appointmentRepository;
            _externalAppointmentRepository = externalAppointmentRepository;
        }
        public async Task<IEnumerable<CustomerShortDTO>> ExecuteAsync(Guid professionalId)
        {
            var customers = await _internalAppointmentRepository.GetCustomersByProfessionalId(professionalId);
            var externalCustomers = await _externalAppointmentRepository.GetCustomersByProfessionalId(professionalId);

            var customerDTOs = _mapper.Map<IEnumerable<CustomerShortDTO>>(customers);
            var externalCustomerDTOs = _mapper.Map<IEnumerable<CustomerShortDTO>>(externalCustomers);

            return customerDTOs.Concat(externalCustomerDTOs);
        }

    }
}
