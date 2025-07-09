using Application.Exceptions;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.ExternalCustomers;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.ExternalCustomers
{
    public class GetExternalCustomerByEmail: IGetExternalCustomerByEmail
    {
        private readonly IExternalCustomerRepository _repository;
        public GetExternalCustomerByEmail(IExternalCustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
        }

        public async Task<ExternalCustomer> Execute(string email, Guid professionalId)
        {
            ExternalCustomer ret = await _repository.GetByEmailAsync(email, professionalId);
            if (ret == null)
            {
                throw new EntityNotFoundException($"No external customer was found with the email '{email}'.");
            }
            return ret;
        }
    }
}
