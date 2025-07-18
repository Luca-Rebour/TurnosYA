using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.UseCases.ExternalClients
{
    public interface IGetExternalClientByPhone
    {
        Task<ExternalClient> Execute(string phone, Guid professionalId);
    }
}
