﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationServiceAPI.Models.Requests
{
    public class UserRequestDto
    {
        public string Email { get; set; }
        public string? Password { get; set; }
        
    }
}
