using Application.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.Customers
{
    public interface IGetCustomerByEmail
    {
        Task<CustomerDTO> ExecuteAsync(string email);
    }
}
