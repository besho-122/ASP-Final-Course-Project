using Besho.DAL.DTO.Requests;
using Besho.DAL.DTO.Responses;
using Besho.DAL.Models;
using Besho.DAL.Repositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository CategoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

     
        public int CrateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            return CategoryRepository.Add(category);

        }

        public int DeleteCategory(int id)
        {
            var category = CategoryRepository.GetById(id);
            if (category is null)return 0;  
            return CategoryRepository.Remove(category); 
        }

        public IEnumerable<CategoryResponses> GetAllCategories()
        {
            var categories = CategoryRepository.GetAll();   
            return categories.Adapt<IEnumerable<CategoryResponses>>();
            
        }

        public CategoryResponses? GetCategoryById(int id)
        {
            var category = CategoryRepository.GetById(id);
            return category is null ? null : category.Adapt<CategoryResponses>();
        }

        public int UpdateCategory(int id, CategoryRequest request)
        {
           var category =CategoryRepository.GetById(id);
            if (category is null) return 0;

            category.Name = request.Name;
            return CategoryRepository.Update(category);

           
        }

        public bool ToggleStatus (int id)
        {
            var category = CategoryRepository.GetById(id);
            if (category is null) return false;
            category.Status=category.Status==Status.Active?Status.Inactive:Status.Active;
            CategoryRepository.Update(category);    
            return true;    
        }



    }
}
