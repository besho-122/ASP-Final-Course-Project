using Besho.BLL.Services.Interfaces;
using Besho.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Besho.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("status/{status}")]   
        public async Task<IActionResult> GetOrdersByStatus([FromRoute] OrderStatus status)
        {
           
            var orders = await _orderService.GetByStatusAsync(status);
            return Ok(orders);
        }


        [HttpPatch("change-status/{orderid}")]
        public async Task <IActionResult> ChangeOrderStatus([FromRoute] int orderid, [FromBody] OrderStatus status)
        {
            var isChanged = await _orderService.ChangeStatusAsync(orderid, status);
            if (!isChanged)
                return BadRequest("Status not changed");
            return Ok("Status changed successfully");
        }       




    }
}
