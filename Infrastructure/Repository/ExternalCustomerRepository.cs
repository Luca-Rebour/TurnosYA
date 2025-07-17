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
    public class ExternalCustomerRepository : IExternalCustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public ExternalCustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ExternalCustomer> AddAsync(ExternalCustomer externalCustomer)
        {

            await _context.ExternalCustomers.AddAsync(externalCustomer);
            await _context.SaveChangesAsync();
            return externalCustomer;
        }

        public async Task<ExternalCustomer> GetByEmailAsync(string email, Guid professionalId)
        {
            ExternalCustomer? externalCustomer = await _context.ExternalCustomers
                                                 .FirstOrDefaultAsync(ec =>
                                                  ec.Email == email &&
                                                  ec.CreatedByProfessionalId == professionalId);

            return externalCustomer;
        }

        public async Task<ExternalCustomer> GetByIdAsync(Guid id)
        {
            ExternalCustomer? externalCustomer = await _context.ExternalCustomers.FirstOrDefaultAsync(x => x.Id == id);

            return externalCustomer;
        }

        public async Task<ExternalCustomer> GetByPhoneAsync(string phone, Guid professionalId)
        {
            ExternalCustomer? externalCustomer = await _context.ExternalCustomers
                                     .FirstOrDefaultAsync(ec =>
                                      ec.Phone == phone &&
                                      ec.CreatedByProfessionalId == professionalId);
            return externalCustomer;
        }

        public async Task<IEnumerable<ExternalCustomer>> GetByProfessionalIdAsync(Guid professionalId)
        {
            IEnumerable<ExternalCustomer> externalCustomers = await _context.ExternalCustomers
                .Where(ec => ec.CreatedByProfessionalId == professionalId)
                .ToListAsync();
            return externalCustomers;
        }

    }
}
