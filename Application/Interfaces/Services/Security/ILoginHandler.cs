using Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Security
{
    public interface ILoginHandler
    {
        Task<LoginResponseDTO?> TryLoginAsync(string email, string password);
    }
}
