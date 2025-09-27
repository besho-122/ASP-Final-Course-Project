using Besho.DAL.DTO.Responses;
using Besho.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task <List<UserDTO>> GetAllAsync();  
        Task <UserDTO> GetByIdAsync(string userId);

        Task<bool> BlockUserAsync(string userId, int days);
        Task<bool> UnBlockUserAsync(string userId);
        Task<bool> IsUserBlockedAsync(string userId);
        Task<bool> ChangeUserRole(string userId, string roleName);
    }
}
