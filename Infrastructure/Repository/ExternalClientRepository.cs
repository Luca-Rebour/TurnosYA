using Application.Interfaces.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ExternalClientRepository : IExternalClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ExternalClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ExternalClient> AddAsync(ExternalClient externalClient)
        {
            await _context.ExternalClients.AddAsync(externalClient);
            await _context.SaveChangesAsync();
            return externalClient;
        }

        public async Task<ExternalClient> GetByEmailAsync(string email, Guid professionalId)
        {
            ExternalClient? externalClient = await _context.ExternalClients
                                                 .FirstOrDefaultAsync(ec =>
                                                  ec.Email == email &&
                                                  ec.CreatedByProfessionalId == professionalId);

            return externalClient;
        }

        public async Task<ExternalClient> GetByIdAsync(Guid id)
        {
            ExternalClient? externalClient = await _context.ExternalClients.FirstOrDefaultAsync(x => x.Id == id);

            return externalClient;
        }

        public async Task<ExternalClient> GetByPhoneAsync(string phone, Guid professionalId)
        {
            ExternalClient? externalClient = await _context.ExternalClients
                                     .FirstOrDefaultAsync(ec =>
                                      ec.Phone == phone &&
                                      ec.CreatedByProfessionalId == professionalId);
            return externalClient;
        }

        public async Task<IEnumerable<ExternalClient>> GetByProfessionalIdAsync(Guid professionalId)
        {
            IEnumerable<ExternalClient> externalClients = await _context.ExternalClients
                .Where(ec => ec.CreatedByProfessionalId == professionalId)
                .ToListAsync();
            return externalClients;
        }

    }
}
