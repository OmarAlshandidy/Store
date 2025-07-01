using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servies.Abstractions;
using Shared.AuthDto;
using Shared.OrderModels;

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
        [HttpGet("EmailExists")] // Get : /api/auth/EmailExists?email=Omar@gmail.com
        public async  Task<IActionResult> CheckEmailExist(string email)
        {
            var result = await serviceManger.authService.CheckEmailExitsAsync(email);
            return Ok(result);
        }

        [HttpGet] // Get :/Api / Auth
        [Authorize]
        public async  Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManger.authService.GetCurrentUserAsyns(email);
            return Ok(result);
        }
        [HttpGet("Address")] //Get :/api/auth/address
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManger.authService.GetCurrentUserAddressAsyns(email);
            return Ok(result);
        }
        [HttpPut("Address")]//Put :/api/auth/address
        [Authorize]
        public async Task<IActionResult> UpdateCurrentUserAddress(AddressDto address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManger.authService.UpdateCurrentUserAddressAsyns(address,email);
            return Ok(result);
        }
    }
}
