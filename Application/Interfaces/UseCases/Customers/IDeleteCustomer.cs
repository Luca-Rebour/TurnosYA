using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.Customers
{
    public interface IDeleteCustomer
    {
        Task ExecuteAsync(Guid id);
    }
}
