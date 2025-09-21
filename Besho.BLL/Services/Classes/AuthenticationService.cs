using Besho.BLL.Services.Interfaces;
using Besho.DAL.DTO.Requests;
using Besho.DAL.DTO.Responses;
using Besho.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.BLL.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(UserManager<ApplicationUser>userManager)
        {
            _userManager = userManager;
        }
        public Task<UserResponse> LoginAsync(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            var user = new ApplicationUser()
            {
                FullName = registerRequest.FullName,
                Email = registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber,
                UserName = registerRequest.UserName
            };
            var Result =await _userManager.CreateAsync(user,registerRequest.Password);//for encreption 
            if (Result.Succeeded) {
                return new UserResponse()
                {
                    Email = registerRequest.Email,

                };
            }
            else
            {
            throw new Exception($"{Result.Errors}");
            }


        }
        
    }
}
