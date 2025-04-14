using Aplication.Interfaces.Repository;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class AvailabilitySlotRepository : IAvailabilitySlotRepository
    {
        private readonly ApplicationDbContext _context;

        public AvailabilitySlotRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AvailabilitySlot> AddAsync(AvailabilitySlot newSlot)
        {
            await _context.AvailabilitySlots.AddAsync(newSlot);
            await _context.SaveChangesAsync();
            return newSlot;

        }

        public async Task DeleteAsync(Guid id)
        {
            AvailabilitySlot availabilitySlot = await _context.AvailabilitySlots.FirstOrDefaultAsync(x => x.Id == id);
            if (availabilitySlot != null)
            {
                _context.AvailabilitySlots.Remove(availabilitySlot);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AvailabilitySlot?> GetByIdAsync(Guid id)
        {
            return await _context.AvailabilitySlots.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<AvailabilitySlot> UpdateAsync(Guid id, AvailabilitySlot updatedSlot)
        {
            AvailabilitySlot availabilitySlot = await _context.AvailabilitySlots.FirstOrDefaultAsync(x => x.Id == id);
            if (availabilitySlot != null)
            {
                availabilitySlot.Update(updatedSlot);
            }
            await _context.SaveChangesAsync();
            return updatedSlot;
        }
    }
}
