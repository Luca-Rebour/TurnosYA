using Application.DTOs.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.Clients
{
    public interface IUpdateClient
    {
        Task<ClientDTO> ExecuteAsync(Guid id, UpdateClientDTO updateClientDTO);
    }
}
