
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Servies.Abstractions;
using Shared.AuthDto;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shared;
using Shared.OrderModels;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Services.Services
{
    public class AuthService(UserManager<AppUser> 
        userManager , IMapper mapper
        , IOptions<JwtOptions> options ) : IAuthService
    {

        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UnAuthorizedException();
          var flag = await  userManager.CheckPasswordAsync(user,loginDto.Password);
            if (!flag) throw new UnAuthorizedException();
            return new UserResultDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateJwtTokenAsync(user)
            };
            
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            //  Validate Duplicated Email
            if (await CheckEmailExitsAsync(registerDto.Email)){
                throw new DuplicatedEmailBadRequsetException(registerDto.Email);
            }
            var user = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
            };
             var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description);
                throw new ValidationException(errors);
            }
            return new UserResultDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token =  await GenerateJwtTokenAsync(user)
            };

        }

        private async Task<string> GenerateJwtTokenAsync(AppUser user)
        {


            // Header 
            // Payload 
            // Signature 
            var jwtOpations = options.Value;
            var authClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email)

            };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaim.Add(new Claim(ClaimTypes.Role, role));
                
            }
            
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpations.SecretKey)); // Array Of Bytes 

            var token = new JwtSecurityToken(
                issuer: jwtOpations.Issuer,
                audience: jwtOpations.Audience,
                 claims: authClaim,
                 expires: DateTime.UtcNow.AddDays(jwtOpations.DurationsInDays),
                 signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature)

                );
            //Token 
            return new JwtSecurityTokenHandler().WriteToken(token);


        }
        public async Task<bool> CheckEmailExitsAsync(string email)
        {
             var user = await userManager.FindByEmailAsync(email);
            return  user != null;  
        }

        public async Task<UserResultDto> GetCurrentUserAsyns(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
                if (user is null) throw new UserNotFoundException(email);
            return new UserResultDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateJwtTokenAsync(user)
            };


        }
        public async Task<AddressDto> GetCurrentUserAddressAsyns(string email)
        {
           var user  = await userManager.Users.Include(U=>U.Address).FirstOrDefaultAsync(U=>U.Email == email);
            if (user is null) throw new UserNotFoundException(email);
            var address  =  mapper.Map<AddressDto>(user.Address);
            return address;
        }

        public  async Task<AddressDto> UpdateCurrentUserAddressAsyns(AddressDto address, string email)
        {
            var user = await userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == email);
            if (user is null) throw new UserNotFoundException(email);
            if(user.Address is not null)
            {
                user.Address.FirstName = address.FirstName;
                user.Address.LastName = address.LastName;
                user.Address.Street= address.Street;
                user.Address.City = address.City;   
                user.Address.Country = address.Country;
            }
            else
            {
                var addressResault = mapper.Map<Address>(address);
                user.Address = addressResault;
            }
              await userManager.UpdateAsync(user);
                return address;
        }

    }
}
