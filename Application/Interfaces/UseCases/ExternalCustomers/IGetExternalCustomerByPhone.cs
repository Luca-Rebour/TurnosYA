using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.ExternalCustomers
{
    public interface IGetExternalCustomerByPhone
    {
        Task<ExternalCustomer> Execute(string phone, Guid professionalId);
    }
}
