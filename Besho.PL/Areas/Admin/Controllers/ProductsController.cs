using Besho.BLL.Services.Classes;
using Besho.BLL.Services.Interfaces;
using Besho.DAL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Besho.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }



        [HttpPost("")]
        public IActionResult Create([FromForm] ProductRequest request)
        {
            var result = _productService.CreateFile(request);
            if (result is Task<int> taskResult)
            {
                int createdId = taskResult.GetAwaiter().GetResult();
                if (createdId > 0)
                    return Ok(new { message = "Product created successfully", id = createdId });
                else
                    return BadRequest(new { message = "Failed to create product" });
            }
            return BadRequest(new { message = "Failed to create product" });
        }


        [HttpGet("")]
        public IActionResult GetAll() => Ok(_productService.GetAll());





        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var Product = _productService.GetById(id);
            if (Product is null) return NotFound(new { message = "Product not found" });
            return Ok(Product);
        }




        [HttpPatch("{id}/toggle-status")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            bool Product = _productService.ToggleStatus(id);
            return Product ? Ok(new { message = "Product Status Updated Succesfully" }) : NotFound(new { message = "Product Not Found to Update It's Status " });

        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var delete = _productService.Delete(id);
            return delete > 0 ? Ok(new { message = "Product deleted succesfully", delete }) : NotFound("Product Not Found To Delete It");

        }













    }





}

