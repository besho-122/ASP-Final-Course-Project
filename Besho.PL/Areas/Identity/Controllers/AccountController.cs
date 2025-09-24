using Besho.BLL.Services.Interfaces;
using Besho.DAL.DTO.Requests;
using Besho.DAL.DTO.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Besho.PL.Areas.Identity.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Identity")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService) {
            _authenticationService = authenticationService;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserResponse>> Register(RegisterRequest registerRequest)
        {
            var result = await _authenticationService.RegisterAsync(registerRequest);
            return Ok(result);
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest registerRequest)
        {
            var result = await _authenticationService.LoginAsync(registerRequest);
            return Ok(result);
        }

    }
}

