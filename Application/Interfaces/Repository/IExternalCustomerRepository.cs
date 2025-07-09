using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IExternalCustomerRepository
    {
        Task<ExternalCustomer> GetByIdAsync(Guid id);
        Task<ExternalCustomer> GetByEmailAsync(string email, Guid professionalId);
        Task<ExternalCustomer> GetByPhoneAsync(string phone, Guid professionalId);
        Task<ExternalCustomer> AddAsync(ExternalCustomer externalCustomer);
        Task<IEnumerable<ExternalCustomer>> GetByProfessionalIdAsync(Guid professionalId);
    }
}
