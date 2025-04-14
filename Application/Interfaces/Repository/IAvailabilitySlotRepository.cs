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
            public Task<AvailabilitySlot> AddAsync(AvailabilitySlot newSlot);
            public Task<AvailabilitySlot> UpdateAsync(Guid id, AvailabilitySlot updatedSlot);
            public Task<AvailabilitySlot?> GetByIdAsync(Guid id);
            public Task DeleteAsync(Guid id);

    }
}
