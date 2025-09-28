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
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {

            this._categoryService = categoryService;
        }
        [HttpGet("")]

        public IActionResult GetAll()
        {
            return Ok(_categoryService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var category = _categoryService.GetById(id);
            if (category is null) return NotFound(new { message = "category not found" });
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CategoryRequest request)
        {
            var id = _categoryService.Create(request);
            return CreatedAtAction(nameof(Get), new { id }, new { message = "created succesfully", request });

        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] int id, CategoryRequest request)
        {
            var updated = _categoryService.Update(id, request);
            return updated > 0 ? Ok(new { massage = "Category Updated Succesfully" }) : NotFound(new { massage = "Categort Not Found to Update it " });

        }

        [HttpPatch("{id}/toggle-status")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            bool cat = _categoryService.ToggleStatus(id);
            return cat ? Ok(new { message = "Category Status Updated Succesfully" }) : NotFound(new { message = "Category Not Found to Update It's Status " });

        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var delete = _categoryService.Delete(id);
            return delete > 0 ? Ok(new { message = "Category deleted succesfully", delete }) : NotFound("Category Not Found To Delete It");

        }




    }
}
