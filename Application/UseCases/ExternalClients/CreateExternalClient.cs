using Application.DTOs.Client;
using Application.DTOs.ExternalClient;
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
    public class CreateExternalClient : ICreateExternalClient
    {
        private readonly IExternalClientRepository _repository;
        private readonly IMapper _mapper;

        public CreateExternalClient(IExternalClientRepository repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ExternalClient> Execute(CreateExternalClientDTO dto)
        {
            dto.Validate();
            ExternalClient newExternalClient = _mapper.Map<ExternalClient>(dto);
            await _repository.AddAsync(newExternalClient);
            return newExternalClient;
        }
    }
}
