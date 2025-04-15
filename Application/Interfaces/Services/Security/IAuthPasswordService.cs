using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Security
{
    public interface IAuthPasswordService
    {
        bool VerifyPassword(string hashedPassword, string inputPassword);
        string HashPassword(string plainPassword);
    }
}
