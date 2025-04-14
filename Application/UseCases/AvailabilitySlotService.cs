using Aplication.Interfaces.Repository;
using Application.DTOs.Availability;
using Application.DTOs.Availibility;
using Application.DTOs.Professional;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class AvailabilitySlotService : IAvailabilitySlotService
    {
        private IAvailabilitySlotRepository _repository;
        private IProfessionalRepository _professionalRepository;
        private IMapper _mapper;
        public AvailabilitySlotService(IAvailabilitySlotRepository repository, IMapper mapper, IProfessionalRepository professionalRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _professionalRepository = professionalRepository;
        }

        public async Task<ProfessionalDTO> Create(CreateAvailabilitySlotDTO dto)
        {
            Professional professional = await _professionalRepository.GetByIdAsync(dto.ProfessionalId);
            AvailabilitySlot newSlot = _mapper.Map<AvailabilitySlot>(dto);
            newSlot.SetProfessional(professional);
            await _repository.AddAsync(newSlot);            
            return _mapper.Map<ProfessionalDTO>(professional);
        }


        public async Task Delete(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ProfessionalDTO> Update(Guid id, UpdateAvailabilitySlotDTO availabilitySlotDTO)
        {
            AvailabilitySlot availabilitySlot = await _repository.GetByIdAsync(id);
            _mapper.Map(availabilitySlotDTO, availabilitySlot);
            await _repository.UpdateAsync(id, availabilitySlot);
            Professional professional = await _professionalRepository.GetByIdAsync(availabilitySlot.ProfessionalId);
            return _mapper.Map<ProfessionalDTO>(professional);

        }
    }
}
