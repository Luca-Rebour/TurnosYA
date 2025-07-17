using Application.DTOs.Availibility;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Repository
{
    public interface IProfessionalRepository
    {
        public Task<Professional> AddAsync(Professional newProfessional);
        public Task<Professional> UpdateAsync(Guid id, Professional updatedProfessional);
        public Task<Professional> GetByIdAsync(Guid id);
        public void UpdateAvailabilityAsync(Guid professionalId, List<AvailabilitySlot> slots);
        public Task<Professional> GetByMailAsync(string mail);
        public Task<int> GetActiveClients(Guid professionalId);
        public Task DeleteAsync(Guid id);
    }
}