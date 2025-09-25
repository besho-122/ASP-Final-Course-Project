using Besho.BLL.Services.Classes;
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
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {

            this._brandService = brandService;
        }

         [HttpGet("")]

        public IActionResult GetAll()
        {
            return Ok(_brandService.GetAll(true));
        }



        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var brand = _brandService.GetById(id);
            if (brand is null) return NotFound(new { message = "brand not found" });
            return Ok(brand);
        }




        [HttpPost("")]
        public IActionResult Create([FromForm] BrandRequest request)
        {

            var result = _brandService.CreateFile(request);
            return Ok(result);


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
