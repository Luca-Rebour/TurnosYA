using Aplication.Interfaces.Repository;
using Application.DTOs.Availibility;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ProfessionalRepository : IProfessionalRepository
    {
        private ApplicationDbContext _context;
        public ProfessionalRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Professional> AddAsync(Professional newProfessional)
        {
            await _context.Professionals.AddAsync(newProfessional);
            await _context.SaveChangesAsync();
            return newProfessional;
        }

        public async Task DeleteAsync(Guid id)
        {
            Professional professional = await _context.Professionals.FindAsync(id);
            if (professional == null)
            {
                _context.Professionals.Remove(professional);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetActiveClients(Guid professionalId)
        {
            var today = DateTime.Today;
            var lastPeriod = today.AddDays(-60);

            var internalActiveClients = await _context.InternalAppointments
                .Where(a => a.ProfessionalId == professionalId &&
                            (a.Date >= lastPeriod))
                .Select(a => a.CustomerId)
                .Distinct()
                .ToListAsync();

            var externalActiveClients = await _context.ExternalAppointments
                .Where(a => a.ProfessionalId == professionalId &&
                            (a.Date >= lastPeriod))
                .Select(a => a.ExternalCustomerId)
                .Distinct()
                .ToListAsync();

            var totalActiveClients = internalActiveClients.Count + externalActiveClients.Count;

            return totalActiveClients;

        }

        public async Task<Professional> GetByIdAsync(Guid id)
        {
            return  await _context.Professionals
                .Include(p => p.Availability)
                .FirstOrDefaultAsync(p => p.Id == id);


        }

        public async Task<Professional> GetByMailAsync(string email)
        {

            Professional professional = await _context.Professionals.FirstOrDefaultAsync(x => x.Email == email);
            return professional;
        }

        public async Task<Professional> UpdateAsync(Guid id, Professional updatedProfessional)
        {
            Professional professional = await _context.Professionals.FindAsync(id);
            if (professional != null)
            {
                professional.Update(updatedProfessional);
                await _context.SaveChangesAsync();
            }
            return professional;
        }

        public async void UpdateAvailabilityAsync(Guid professionalId, List<AvailabilitySlot> slots)
        {
            Professional professional = await _context.Professionals.FindAsync(professionalId);
            professional.UpdateAvailability(slots);

        }

    }
}
