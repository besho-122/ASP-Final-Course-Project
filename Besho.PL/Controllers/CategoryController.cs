using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Besho.DAL.Models;
using Besho.DAL.Data;

namespace Besho.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
    }

}
