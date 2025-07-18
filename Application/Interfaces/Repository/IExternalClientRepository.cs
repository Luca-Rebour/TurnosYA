using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IExternalClientRepository
    {
        Task<ExternalClient> GetByIdAsync(Guid id);
        Task<ExternalClient> GetByEmailAsync(string email, Guid professionalId);
        Task<ExternalClient> GetByPhoneAsync(string phone, Guid professionalId);
        Task<ExternalClient> AddAsync(ExternalClient externalClient);
        Task<IEnumerable<ExternalClient>> GetByProfessionalIdAsync(Guid professionalId);
    }
}
