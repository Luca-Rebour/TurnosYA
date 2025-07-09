using Application.Exceptions;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.ExternalCustomers;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.ExternalCustomers
{
    public class GetExternalCustomersByProfessionalId: IGetExternalCustomersByProfessionalId
    {
        private readonly IExternalCustomerRepository _repository;
        public GetExternalCustomersByProfessionalId(IExternalCustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ExternalCustomer>> Execute(Guid professionalId)
        {
            IEnumerable<ExternalCustomer> ret = await _repository.GetByProfessionalIdAsync(professionalId);
            if (ret == null)
            {
                throw new EntityNotFoundException($"No external customers were found with the professonal id '{professionalId}'.");
            }
            return ret;
        }
    }
}
