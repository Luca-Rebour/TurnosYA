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
    public class GetProfessionalById: IGetProfessionalById
    {
        private IProfessionalRepository _repository;
        private IMapper _mapper;
        public GetProfessionalById(IProfessionalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ProfessionalDTO> ExecuteAsync(Guid id)
        {
            Professional professional = await _repository.GetByIdAsync(id);

            if (professional == null)
            {
                throw new Exception("Professional not found");
            }

            return _mapper.Map<ProfessionalDTO>(professional);
        }
    }
}
