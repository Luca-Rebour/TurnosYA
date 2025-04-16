using Application.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<CustomerDTO> Create(CreateCustomerDTO customerDTO);
        Task<CustomerDTO> Update(Guid id, UpdateCustomerDTO customerDTO);
        Task<CustomerDTO> GetById(Guid id);
        Task<CustomerDTO> GetByEmail(string email);
        Task<CustomerInternalDTO> GetByEmailInternal(string email);
        Task Delete(Guid id);


    }
}
