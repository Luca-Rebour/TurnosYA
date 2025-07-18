using Application.DTOs.Availability;
using Application.DTOs.Availibility;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Repository
{
    public interface IAvailabilitySlotRepository
    {
            Task<AvailabilitySlot> AddAsync(AvailabilitySlot newSlot);
            Task<IEnumerable<AvailabilitySlot>> GetAllAsync(Guid professionalId);
            Task<AvailabilitySlot> UpdateAsync(Guid id, AvailabilitySlot updatedSlot);
            Task<AvailabilitySlot?> GetByIdAsync(Guid id);
            Task DeleteAsync(Guid id);
            Task AddRangeAsync(IEnumerable<AvailabilitySlot> slots);
            Task<AvailabilitySlot?> GetAvailableSlotAsync(Guid professionalId, DayOfWeek day, TimeSpan time);
            Task<IEnumerable<AvailabilitySlot>> GetByDateAsync(Guid professionalId, DateTime date);

    }
}
