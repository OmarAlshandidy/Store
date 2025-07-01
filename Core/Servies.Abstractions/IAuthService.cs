using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.AuthDto;
using Shared.OrderModels;

namespace Servies.Abstractions
{
    public interface IAuthService
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);

        //Check Email Exists 
      Task<bool>  CheckEmailExitsAsync(string  email);

        //Get Current User 
        Task<UserResultDto> GetCurrentUserAsyns(string email);

        // Get Current User Address 
        Task<AddressDto> GetCurrentUserAddressAsyns(string email);
        // Update Current User Address 

        Task<AddressDto> UpdateCurrentUserAddressAsyns(AddressDto address, string email);


    }
}
