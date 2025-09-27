using Besho.DAL.DTO.Requests;
using Besho.DAL.DTO.Responses;
using Besho.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.BLL.Services.Interfaces
{
    public interface IProductService:IGenericService<ProductRequest,ProductResponse,Product>
    {
        Task<int> CreateFile(ProductRequest request);
        Task<List<ProductResponse>> GetAllProduct(HttpRequest request, bool onlyActive = false, int pageNumber = 1, int pageSize = 1);
    }
}
