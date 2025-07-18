using Application.DTOs.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.Clients
{
    public interface IGetClientById
    {
        Task<ClientDTO> ExecuteAsync(Guid id);
    }
}
