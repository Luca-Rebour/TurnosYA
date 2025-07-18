using Application.Interfaces.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ClientRepository : IClientRepository
    {
        private ApplicationDbContext _context;
        public ClientRepository(ApplicationDbContext context)
        {
         _context = context;
        }
        public async Task<Client> AddAsync(Client newClient)
        {
            await _context.Clients.AddAsync(newClient);
            await _context.SaveChangesAsync();
            return newClient; 
        }

        public async Task DeleteAsync(Guid id)
        {
            Client client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                _context.Clients.Remove(client);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Client> GetByIdAsync(Guid id)
        {
            Client client = await _context.Clients.FindAsync(id);
            return client;

        }

        public async Task<Client?> GetByMailAsync(string email)
        {
            Client? client = await _context.Clients.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

            return client;
        }


        public async Task<Client> UpdateAsync(Guid id, Client updatedClient)
        {
            Client client = _context.Clients.FirstOrDefault(x => x.Id == id);
            if (client != null)
            {
                client.Update(updatedClient);
                await _context.SaveChangesAsync();
            }
            return client;
        }
    }
}
