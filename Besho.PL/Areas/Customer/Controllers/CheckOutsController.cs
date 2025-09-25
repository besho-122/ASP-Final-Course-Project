using Besho.BLL.Services.Interfaces;
using Besho.DAL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Besho.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CheckOutsController : ControllerBase
    {
        private readonly ICheckOutService _checkOutService;

        public CheckOutsController(ICheckOutService checkOutService)
        {
            _checkOutService = checkOutService;
        }

        [HttpPost("payment")]

        public async Task<IActionResult> Payment([FromBody]CheckOutRequest request)
        {
          var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _checkOutService.ProcessPaymentAsync(request, userId, Request);
            return Ok(response);
        }

        [HttpGet("Success/{orderId}")]
        [AllowAnonymous]
        public async Task<IActionResult> SuccessAsync([FromRoute]int orderId)
        {
            var result =await _checkOutService.HandlePaymentSuccessAsync(orderId);    
            return Ok(result);
        }


        [HttpGet("Cancel")]
        [AllowAnonymous]
        public IActionResult Cancel()
        {

            return Ok("Cancel");
        }






    }
}
