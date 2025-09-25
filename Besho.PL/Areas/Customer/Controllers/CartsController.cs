using Besho.BLL.Services.Interfaces;
using Besho.DAL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Security.Claims;

namespace Besho.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService) {
            _cartService = cartService;
        }

        [HttpPost("")]
        public async  Task<IActionResult> AddToCart(CartRequest request)
        {
          var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result =await _cartService.AddToCartAsync(request,userId);
            return result ? Ok() : BadRequest();

        }

        [HttpGet("")]
        public async Task<IActionResult> GetUserCart()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.CartSummaryResponseAsync(UserId);
            return Ok(result);  
        }
    }
}
