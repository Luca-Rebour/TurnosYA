﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Users
{
    public interface IValidateUserEmail
    {
        Task<bool> ExecuteAsync(string email);
    }
}
