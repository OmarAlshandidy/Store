﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.AuthDto;

namespace Servies.Abstractions
{
    public interface IAuthService
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
    }
}
