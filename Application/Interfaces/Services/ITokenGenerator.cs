using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ITokenGenerator
    {
        string GenerateToken(string userId, string name, string role);
    }
}
