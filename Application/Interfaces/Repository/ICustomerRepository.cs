using Application.DTOs.Customer;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface ICustomerRepository
    {
        public Task<Customer> AddAsync(Customer newCustomer);
        public Task<Customer> UpdateAsync(Guid id, Customer updatedCustomer);
        public Task<Customer> GetByIdAsync(Guid id);
        public Task<Customer> GetByMailAsync(string mail);
        public Task DeleteAsync(Guid id);

    }
}
