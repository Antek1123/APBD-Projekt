﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektApbd.Shared.Models.DTOs
{
    public class UserLoginResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string JwtToken { get; set; } = string.Empty;
    }
}
