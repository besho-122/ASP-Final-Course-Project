using Besho.DAL.DTO.Requests;
using Besho.DAL.DTO.Responses;
using Besho.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.BLL.Services.Interfaces
{
    public interface IBrandService:IGenericService<BrandRequest,BrandResponses,Brand>
    {
        
    }
}
