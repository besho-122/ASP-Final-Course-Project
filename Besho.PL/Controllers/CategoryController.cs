using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Besho.DAL.Models;
using Besho.DAL.Data;
using Besho.BLL.Services;
using Besho.DAL.DTO.Requests;

namespace Besho.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpGet("")]
        public IActionResult GetAll() {
            return Ok(categoryService.GetAllCategories());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id) {
            var category = categoryService.GetCategoryById(id);
            if (category is null) return NotFound(new {message="category not found"});
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Create([FromBody]CategoryRequest request)
        {
           var id= categoryService.CrateCategory(request);
            return CreatedAtAction(nameof(Get),new {id});
           
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] int id , CategoryRequest request)
        {
            var updated = categoryService.UpdateCategory(id, request);
            return updated > 0 ? Ok(new {massage="Category Updated Succesfully"}) : NotFound(new {massage="Categort Not Found to Update it "});

        }

        [HttpPatch("{id}/toggle-status")]
        public IActionResult ToggleStatus([FromRoute] int id) {
            bool cat = categoryService.ToggleStatus(id);
            return cat ? Ok(new {message="Category Status Updated Succesfully"}) : NotFound(new {message="Category Not Found to Update It's Status "});

        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            var delete = categoryService.DeleteCategory(id);
            return delete > 0 ? Ok(new {message="Category deleted succesfully",delete}) : NotFound("Category Not Found To Delete It");

        }



        


    }

}
