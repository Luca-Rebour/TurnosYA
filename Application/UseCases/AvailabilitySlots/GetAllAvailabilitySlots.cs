using Aplication.Interfaces.Repository;
using Application.DTOs.Availibility;
using Application.Interfaces.UseCases.AvailabilitySlots;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.AvailabilitySlots
{
    public class GetAllAvailabilitySlots: IGetAllAvailabilitySlots
    {
        private readonly IAvailabilitySlotRepository _availabilitySlotRepository;
        public GetAllAvailabilitySlots(IAvailabilitySlotRepository availabilitySlotRepository)
        {
            _availabilitySlotRepository = availabilitySlotRepository;
        }

        public async Task<IEnumerable<AvailabilitySlot>> Execute(Guid professionalId)
        {
            return await _availabilitySlotRepository.GetAllAsync(professionalId);
        }
    }
}
