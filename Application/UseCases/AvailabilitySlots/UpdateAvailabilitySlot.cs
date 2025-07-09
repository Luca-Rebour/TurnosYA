using Aplication.Interfaces.Repository;
using Application.DTOs.Availibility;
using Application.DTOs.Professional;
using Application.Interfaces.UseCases.Appointments;
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
    public class UpdateAvailabilitySlot: IUpdateAvailabilitySlot
    {
        private IAvailabilitySlotRepository _repository;
        private IProfessionalRepository _professionalRepository;
        private IMapper _mapper;
        public UpdateAvailabilitySlot(IAvailabilitySlotRepository repository, IProfessionalRepository professionalRepository, IMapper mapper)
        {
            _repository = repository;
            _professionalRepository = professionalRepository;
            _mapper = mapper;
        }
        public async Task<ProfessionalDTO> ExecuteAsync(Guid id, UpdateAvailabilitySlotDTO availabilitySlotDTO)
        {
            AvailabilitySlot availabilitySlot = await _repository.GetByIdAsync(id);
            _mapper.Map(availabilitySlotDTO, availabilitySlot);
            await _repository.UpdateAsync(id, availabilitySlot);
            Professional professional = await _professionalRepository.GetByIdAsync(availabilitySlot.ProfessionalId);
            return _mapper.Map<ProfessionalDTO>(professional);

        }
    }
}
