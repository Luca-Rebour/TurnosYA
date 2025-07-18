using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.ExternalClients
{
    public interface IGetExternalClientByEmail
    {
        Task<ExternalClient> Execute(string email, Guid professionalId);
    }
}
