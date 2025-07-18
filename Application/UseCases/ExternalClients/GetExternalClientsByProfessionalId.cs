using Application.Exceptions;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.ExternalClients;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.ExternalClients
{
    public class GetExternalClientsByProfessionalId: IGetExternalClientsByProfessionalId
    {
        private readonly IExternalClientRepository _repository;
        public GetExternalClientsByProfessionalId(IExternalClientRepository repository, IMapper mapper)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ExternalClient>> Execute(Guid professionalId)
        {
            IEnumerable<ExternalClient> ret = await _repository.GetByProfessionalIdAsync(professionalId);
            if (ret == null)
            {
                throw new EntityNotFoundException($"No external clients were found with the professonal id '{professionalId}'.");
            }
            return ret;
        }
    }
}
