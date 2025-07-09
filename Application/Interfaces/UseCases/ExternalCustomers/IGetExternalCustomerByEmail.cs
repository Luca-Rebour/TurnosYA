using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.ExternalCustomers
{
    public interface IGetExternalCustomerByEmail
    {
        Task<ExternalCustomer> Execute(string email, Guid professionalId);
    }
}
