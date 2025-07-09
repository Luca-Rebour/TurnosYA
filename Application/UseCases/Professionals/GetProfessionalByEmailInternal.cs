using Aplication.Interfaces.Repository;
using Application.DTOs.Professional;
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
    public class GetProfessionalByEmailInternal: IGetProfessionalByEmailInternal
    {
        private IProfessionalRepository _repository;
        private IMapper _mapper;
        public GetProfessionalByEmailInternal(IProfessionalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ProfessionalInternalDTO> ExecuteAsync(string email)
        {
            Professional professional = await _repository.GetByMailAsync(email);


            return _mapper.Map<ProfessionalInternalDTO>(professional);
        }
    }
}
