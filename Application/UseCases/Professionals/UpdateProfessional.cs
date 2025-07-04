using Aplication.Interfaces.Repository;
using Application.DTOs.Professional;
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
    public class UpdateProfessional: IUpdateProfessional
    {
        private IProfessionalRepository _repository;
        private IMapper _mapper;
        public UpdateProfessional(IProfessionalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ProfessionalDTO> ExecuteAsync(Guid id, UpdateProfessionalDTO dto)
        {
            Professional professional = await _repository.GetByIdAsync(id);
            if (professional == null)
            {
                throw new Exception("Profesional no encontrado");
            }

            _mapper.Map(dto, professional);

            await _repository.UpdateAsync(id, professional);

            return _mapper.Map<ProfessionalDTO>(professional);
        }
    }
}
