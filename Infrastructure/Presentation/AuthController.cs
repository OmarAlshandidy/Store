using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Servies.Abstractions;
using Shared.AuthDto;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController(IServiceManger serviceManger) :ControllerBase
    {
        [HttpPost("login")] //Post: /api/auth/login
        public async Task<IActionResult> Login(LoginDto loginDto)
        { 
            var result = await serviceManger.authService.LoginAsync(loginDto);
            return Ok (result);

        }
        [HttpPost("register")] //Post: /api/auth/login
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await serviceManger.authService.RegisterAsync(registerDto);
            return Ok(result);

        }
    }
}
