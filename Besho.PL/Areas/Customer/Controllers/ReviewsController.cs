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
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewsController(IReviewService service)
        {
            _service = service;
        }
        [HttpPost("")]
        public async Task<IActionResult> AddReview([FromBody]ReviewRequset reviewRequset)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ressult =await _service.AddReviewAsync(reviewRequset, userId);  
            return Ok(ressult);    

        }
    }
}
