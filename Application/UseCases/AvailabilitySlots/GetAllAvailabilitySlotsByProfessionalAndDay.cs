using Aplication.Interfaces.Repository;
using Application.Interfaces.UseCases.AvailabilitySlots;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.AvailabilitySlots
{
    public class GetAllAvailabilitySlotsByProfessionalAndDay : IGetAllAvailabilitySlotsByProfessionalAndDay
    {
        private readonly IAvailabilitySlotRepository _availabilitySlotRepository;
        public GetAllAvailabilitySlotsByProfessionalAndDay(IAvailabilitySlotRepository availabilitySlotRepository)
        {
            _availabilitySlotRepository = availabilitySlotRepository;
        }

        public async Task<IEnumerable<AvailabilitySlot>> Execute(Guid professionalId, DateTime date)
        {
            IEnumerable<AvailabilitySlot> slots = await _availabilitySlotRepository.GetByDateAsync(professionalId, date);
            return slots;
        }
    }
}
