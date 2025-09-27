using Besho.BLL.Services.Interfaces;
using Besho.DAL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Besho.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("")]   
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user is null) return NotFound(new { message = "User not found" });
            return Ok(user);
        }

        [HttpPatch("block/{userId}")]
        public async Task<IActionResult> BlockUser([FromRoute] string userId, [FromBody] int days)
        {
            var result = await _userService.BlockUserAsync(userId, days);
            if (!result) return NotFound(new { message = "User not found or could not be blocked" });
            return Ok(new { message = "User blocked successfully",result });
        }

        [HttpPatch("unblock/{userId}")]
        public async Task<IActionResult> UnBlockUser([FromRoute] string userId)
        {
            var result = await _userService.UnBlockUserAsync(userId);
            if (!result) return NotFound(new { message = "User not found or could not be unblocked" });
            return Ok(new { message = "User unblocked successfully", result });
        }


        [HttpPatch("isblock/{userId}")]
        public async Task<IActionResult> IsBlockUser([FromRoute] string userId)
        {
            var result = await _userService.IsUserBlockedAsync(userId);
            if (!result) return NotFound(new { message = "User not found or could not be check for blocked " });
            return Ok(new { message = "is user bloked:", result });
        }



        [HttpPatch("changeRole/{userId}")]
        public async Task<IActionResult> ChangeUserRole([FromRoute] string userId, [FromBody] ChangeRoleRequest request)
        {
          
            var result = await _userService.ChangeUserRole(userId, request.RoleName);
            if (!result) return NotFound(new { message = "User not found or role could not be changed" });
            return Ok(new { message = "User role changed successfully", result });
        }   

    }
}
