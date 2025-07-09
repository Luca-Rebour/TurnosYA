using Application.DTOs.ExternalCustomer;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.ExternalCustomers
{
    public interface ICreateExternalCustomer
    {
    Task<ExternalCustomer> Execute(CreateExternalCustomerDTO dto);
    }
}
