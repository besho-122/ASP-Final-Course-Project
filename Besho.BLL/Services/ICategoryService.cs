using Besho.DAL.DTO.Requests;
using Besho.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.BLL.Services
{
  public  interface ICategoryService
    {
        int CrateCategory(CategoryRequest request);
        IEnumerable<CategoryResponses> GetAllCategories();

        CategoryResponses? GetCategoryById(int id);

        int UpdateCategory(int id ,CategoryRequest request);

        int DeleteCategory(int id);

        bool ToggleStatus(int id);






    }
}
