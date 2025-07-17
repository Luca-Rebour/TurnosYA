using Aplication.Interfaces.Repository;
using Application.DTOs.Availability;
using Application.DTOs.Professional;
using Application.Interfaces.Repository;
using Application.Interfaces.UseCases.AvailabilitySlots;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.AvailabilitySlots
{
    public class CreateAvailabilitySlot: ICreateAvailabilitySlot
    {
        private IAvailabilitySlotRepository _repository;
        private IProfessionalRepository _professionalRepository;
        private IMapper _mapper;
        public CreateAvailabilitySlot(IAvailabilitySlotRepository repository, IProfessionalRepository professionalRepository, IMapper mapper)
        {
            _repository = repository;
            _professionalRepository = professionalRepository;
            _mapper = mapper;
        }
        public async Task<ProfessionalDTO> ExecuteAsync(CreateAvailabilitySlotDTO dto)
        {
            dto.Validate();
            Professional professional = await _professionalRepository.GetByIdAsync(dto.ProfessionalId);
            AvailabilitySlot newSlot = _mapper.Map<AvailabilitySlot>(dto);
            newSlot.SetProfessional(professional);
            await _repository.AddAsync(newSlot);
            return _mapper.Map<ProfessionalDTO>(professional);
        }
    }
}
