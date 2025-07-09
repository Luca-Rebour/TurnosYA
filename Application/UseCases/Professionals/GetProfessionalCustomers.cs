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
        private IAppointmentRepository _appointmentRepository;
        private IMapper _mapper;
        public GetProfessionalCustomers(IProfessionalRepository repository, IMapper mapper, IAppointmentRepository appointmentRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }
        public async Task<IEnumerable<CustomerShortDTO>> ExecuteAsync(Guid professionalId)
        {
            IEnumerable<Customer> customers = await _appointmentRepository.GetCustomersByProfessionalId(professionalId);
            IEnumerable<CustomerShortDTO> customersDTO = _mapper.Map<IEnumerable<CustomerShortDTO>>(customers);
            return customersDTO;
        }
    }
}
