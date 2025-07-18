using Application.DTOs.ExternalClient;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.ExternalClients
{
    public interface ICreateExternalClient
    {
    Task<ExternalClient> Execute(CreateExternalClientDTO dto);
    }
}
