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

            var result = await _authenticationService.RegisterAsync(registerRequest,Request);
            return Ok(result);
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest registerRequest)
        {
            var result = await _authenticationService.LoginAsync(registerRequest);
            return Ok(result);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult<string>> ConfirmEmail([FromQuery] string token, [FromQuery] string userId)
        {

            var result = await _authenticationService.ConfirmEmail(token,userId);
            return Ok(result);
        }

        [HttpPost("forget-password")]
        public async Task<ActionResult<string>> ForgetPassword([FromBody] ForgetPasswordRequest request)
        {
            var result = await _authenticationService.ForgetPassword(request);
            return Ok(result);
        }

        [HttpPatch("reset-password")]
       public async Task<ActionResult<string>> ResetPassword([FromBody]ResetPasswordRequest request)
        {
            var result =await _authenticationService.ResetPassword(request);
            return Ok(result);
        }
    }
}

