using Application.DTOs.Client;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IClientRepository
    {
        public Task<Client> AddAsync(Client newClient);
        public Task<Client> UpdateAsync(Guid id, Client updatedClient);
        public Task<Client> GetByIdAsync(Guid id);
        public Task<Client> GetByMailAsync(string mail);
        public Task DeleteAsync(Guid id);

    }
}
