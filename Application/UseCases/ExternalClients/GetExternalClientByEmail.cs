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
    public class GetExternalClientByEmail: IGetExternalClientByEmail
    {
        private readonly IExternalClientRepository _repository;
        public GetExternalClientByEmail(IExternalClientRepository repository, IMapper mapper)
        {
            _repository = repository;
        }

        public async Task<ExternalClient> Execute(string email, Guid professionalId)
        {
            ExternalClient ret = await _repository.GetByEmailAsync(email, professionalId);
            if (ret == null)
            {
                throw new EntityNotFoundException($"No external client was found with the email '{email}'.");
            }
            return ret;
        }
    }
}
