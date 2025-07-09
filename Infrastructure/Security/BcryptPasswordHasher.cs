﻿using Application.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class BcryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hashed)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashed);
        }
    }
}
