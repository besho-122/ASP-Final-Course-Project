using Besho.BLL.Services.Interfaces;
using Besho.DAL.DTO.Requests;
using Besho.DAL.DTO.Responses;
using Besho.DAL.Models;
using Besho.DAL.Repositories.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.BLL.Services.Classes
{
    public class CategoryService : GenericService<CategoryRequest,CategoryResponses,Category>,ICategoryService
    {
       public CategoryService(ICategoryRepository repository):base(repository) { }
      


    }
}
