﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UserInternalDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rol {  get; set; }
    }
}
