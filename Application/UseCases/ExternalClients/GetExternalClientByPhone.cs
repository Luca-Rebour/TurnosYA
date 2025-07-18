using Application.Exceptions;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.ExternalClients;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.ExternalClients
{
    public class GetExternalClientByPhone: IGetExternalClientByPhone
    {
        private readonly IExternalClientRepository _repository;
        public GetExternalClientByPhone(IExternalClientRepository repository, IMapper mapper)
        {
            _repository = repository;
        }

        public async Task<ExternalClient> Execute(string phone, Guid professionalId)
        {
            ExternalClient ret = await _repository.GetByPhoneAsync(phone, professionalId);
            if (ret == null)
            {
                throw new EntityNotFoundException($"No external client was found with the phone '{phone}'.");
            }
            return ret;
        }
    }
}
