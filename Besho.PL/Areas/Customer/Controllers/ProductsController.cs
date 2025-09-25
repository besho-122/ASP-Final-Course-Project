using Besho.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Besho.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]

    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {

            this._productService = productService;
        }
        [HttpGet("")]

        public IActionResult GetAll()
        {
            return Ok(_productService.GetAll(false));
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var Product = _productService.GetById(id);
            if (Product is null) return NotFound(new { message = "Product not found" });
            return Ok(Product);
        }


    }
}
