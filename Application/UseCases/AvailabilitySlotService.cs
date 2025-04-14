using Aplication.Interfaces.Repository;
using Application.DTOs.Availability;
using Application.DTOs.Availibility;
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
        private IMapper _mapper;
        public AvailabilitySlotService(IAvailabilitySlotRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AvailabilitySlotDTO> Create(CreateAvailabilitySlotDTO availabilityDTO)
        {
            AvailabilitySlot availabilitySlot = _mapper.Map<AvailabilitySlot>(availabilityDTO);
            await _repository.AddAsync(availabilitySlot);
            return _mapper.Map<AvailabilitySlotDTO>(availabilitySlot);
        }

        public async Task Delete(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<AvailabilitySlotDTO> Update(Guid id, UpdateAvailabilitySlotDTO availabilitySlotDTO)
        {
            AvailabilitySlot availabilitySlot = await _repository.GetByIdAsync(id);
            _mapper.Map(availabilitySlotDTO, availabilitySlot);
            return _mapper.Map<AvailabilitySlotDTO>(availabilitySlot);

        }
    }
}
