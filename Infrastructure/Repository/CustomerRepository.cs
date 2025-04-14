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
    public class CustomerRepository : ICustomerRepository
    {
        private ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context)
        {
         _context = context;
        }
        public async Task<Customer> AddAsync(Customer newCustomer)
        {
            _context.Customers.AddAsync(newCustomer);
            await _context.SaveChangesAsync();
            return newCustomer; 
        }

        public async Task DeleteAsync(Guid id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                _context.Customers.Remove(customer);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            return customer;

        }

        public async Task<Customer?> GetByMailAsync(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Email == email);
        }


        public async Task<Customer> UpdateAsync(Guid id, Customer updatedCustomer)
        {
            Customer customer = _context.Customers.FirstOrDefault(x => x.Id == id);
            if (customer != null)
            {
                customer.Update(updatedCustomer);
                await _context.SaveChangesAsync();
            }
            return customer;
        }
    }
}
