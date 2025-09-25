using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Besho.DAL.Models;
using Besho.DAL.Data;
using Besho.DAL.DTO.Requests;
using Besho.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Besho.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {

            this._brandService = brandService;
        }
        [HttpGet("")]
       
        public IActionResult GetAll()
        {
            return Ok(_brandService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var brand = _brandService.GetById(id);
            if (brand is null) return NotFound(new { message = "brand not found" });
            return Ok(brand);
        }

        [HttpPost]
        public IActionResult Create([FromBody] BrandRequest request)
        {
            var id = _brandService.Create(request);
            return CreatedAtAction(nameof(Get), new { id }, new { message = "created succesfully", request });

        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] int id, BrandRequest request)
        {
            var updated = _brandService.Update(id, request);
            return updated > 0 ? Ok(new { massage = "brand Updated Succesfully" }) : NotFound(new { massage = "brand Not Found to Update it " });

        }

        [HttpPatch("{id}/toggle-status")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            bool brand = _brandService.ToggleStatus(id);
            return brand ? Ok(new { message = "brand Status Updated Succesfully" }) : NotFound(new { message = "brand Not Found to Update It's Status " });

        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var delete = _brandService.Delete(id);
            return delete > 0 ? Ok(new { message = "brand deleted succesfully", delete }) : NotFound("brand Not Found To Delete It");

        }






    }


}
