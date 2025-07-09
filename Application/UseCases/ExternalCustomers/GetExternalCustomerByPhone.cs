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
    public class GetExternalCustomerByPhone: IGetExternalCustomerByPhone
    {
        private readonly IExternalCustomerRepository _repository;
        public GetExternalCustomerByPhone(IExternalCustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
        }

        public async Task<ExternalCustomer> Execute(string phone, Guid professionalId)
        {
            ExternalCustomer ret = await _repository.GetByPhoneAsync(phone, professionalId);
            if (ret == null)
            {
                throw new EntityNotFoundException($"No external customer was found with the phone '{phone}'.");
            }
            return ret;
        }
    }
}
