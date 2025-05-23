﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AuthDto
{
    public class RegisterDto
    {
        public  string  UserName { get; set; }
        public string DisplayName { get; set; }
        [EmailAddress]
        public string Email  { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
