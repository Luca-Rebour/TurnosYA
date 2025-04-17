﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Security
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string name, string role);
    }

}
