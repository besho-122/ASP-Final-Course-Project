using Besho.BLL.Services.Interfaces;
using Besho.DAL.DTO.Responses;
using Besho.DAL.Models;
using Besho.DAL.Repositories.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.BLL.Services.Classes
{
   public  class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUserRepository userRepository,UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

       

        public async Task<List<UserDTO>> GetAllAsync()
        {

            var users  = await _userRepository.GetAllAsync();
            var userDtos =new List<UserDTO>();
            foreach (var user in users)
            {
                var role =await _userManager.GetRolesAsync(user);
                userDtos.Add(new UserDTO
                {
                    Id = user.Id,
                    FullName = user.FullName,   
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    RoleName = role.FirstOrDefault() 
                });

            }   

            return userDtos;


        }

        public async Task<UserDTO> GetByIdAsync(string userId)
        {
         
            var user =await _userRepository.GetByIdAsync(userId);
            return user.Adapt<UserDTO>(); 

        }

        public async Task<bool> BlockUserAsync(string userId, int days)
        {
            return await _userRepository.BlockUserAsync(userId, days);    
        }

        public async Task<bool> IsUserBlockedAsync(string userId)
        {
           return await _userRepository.IsUserBlockedAsync(userId);   
        }

        public async Task<bool> UnBlockUserAsync(string userId)
        {
          return await _userRepository.UnBlockUserAsync(userId);
        }

        public async Task<bool> ChangeUserRole(string userId, string roleName)
        {
          return await _userRepository.ChangeUserRole(userId, roleName);  
        }
    }
}
